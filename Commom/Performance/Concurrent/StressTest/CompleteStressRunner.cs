using System.Collections.Generic;
using Utilities.Common.Performance.DataGeneration;
using Utilities.Common.Performance.DataGeneration.Models;
using Utilities.Common.Settings;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class CompleteContratoStressRunner<T, TKParcela, TKCobranca>
        where T : class, IContrato, new()
        where TKParcela : class, IParcela, new()
        where TKCobranca : class, ICobranca, new()
    {
        private class TesteRuben : StressTestSettings
        {
            public override int TotalDumbInserts { get { return 1000; } }
            public override int TotalInsertsToManipulate { get { return 100; } }
            public override int Updates { get { return (int)(TotalInsertsToManipulate * 0.40f); } }
            public override int Deletes { get { return (int)(TotalInsertsToManipulate * 0.60f); } }
            public override int Selects { get { return TotalInsertsToManipulate * 0; } }
            public override int SelectsInterval { get { return (int)(TotalDumbInserts * 0.10f); } }
            public override int TotalCiclos { get { return 100; } }
        }

        private class TesteComUmMilhao : StressTestSettings
        {
            readonly AppConfig _appConfig = new AppConfig();

            public override int TotalDumbInserts { get { return _appConfig.GetInt32("TotalDumbInserts"); } }
            public override int TotalInsertsToManipulate { get { return _appConfig.GetInt32("TotalInsertsToManipulate"); } }
            public override int Updates { get { return (int)(TotalInsertsToManipulate * _appConfig.GetDecimal("Updates")); } }
            public override int Deletes { get { return (int)(TotalInsertsToManipulate * _appConfig.GetDecimal("Deletes")); } }
            public override int Selects { get { return (int)(TotalInsertsToManipulate * _appConfig.GetDecimal("Selects")); } }
            public override int SelectsInterval { get { return (int)(TotalDumbInserts * _appConfig.GetDecimal("SelectsInterval")); } }
            public override int TotalCiclos { get { return _appConfig.GetInt32("TotalCiclos"); } }
        }

        public void Run(ICrudDataHandleStressable<T> crud, ISelectionStressable<T> select)
        {
            var settings = new TesteComUmMilhao();
            var dadosParaBusca = new StressTestDataCollection<T>();

            var selects = new List<SelectStressRunner<T, TKParcela, TKCobranca>>();

            using (new Cronometro("Executando {0} x {1}", settings.TotalCiclos, settings))
            {
                for (var i = 0; i < settings.TotalCiclos; i++)
                {
                    var stressData = new CrudStressData<T>(settings, new ContratoGeneration<T, TKParcela, TKCobranca>(new DataGenerationSettings()));

                    var percentualParaBusca = stressData.InsertsDumb.TakeRandomWithoutRemove(settings.SelectsInterval);
                    dadosParaBusca.AddRange(percentualParaBusca);

                    var crudRunner = new CrudStressRunner<T>(crud, stressData);

                    using (new Cronometro("Executando inserts"))
                        crudRunner.Run();

                    var selectRunner = new SelectStressRunner<T, TKParcela, TKCobranca>(select, dadosParaBusca);

                    selectRunner.Run();

                    selects.Add((selectRunner));
                }

                foreach (var selectStressRunner in selects)
                    selectStressRunner.WaitAll();
            }
        }
    }
}
