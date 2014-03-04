using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Utilities.Common.Performance.Concurrent.StressTest;
using Utilities.Common.Performance.DataGeneration.Models;

namespace Utilities.Common.Performance.DataGeneration
{
    public class ContratoGeneration<T, TKParcela, TKCobranca> : IDataGenerationProvider<T>
        where T : IContrato, new()
        where TKParcela : IParcela, new()
        where TKCobranca : ICobranca, new()
    {
        private readonly DataGenerationSettings _settings;
        private readonly ParcelaGeneration<TKParcela, TKCobranca> _parcela;
        private readonly Random _rand = new Random(1);

        public ContratoGeneration(DataGenerationSettings settings)
        {
            _settings = settings;
            _parcela = new ParcelaGeneration<TKParcela, TKCobranca>(settings);
        }

        private T GetNew()
        {
            var agora = DateTime.Now.AddSeconds(-_rand.Next(1800));

            var contrato = new T
                               {
                                   Id = Guid.NewGuid(),
                                   Nome = agora.ToString(CultureInfo.InvariantCulture),
                                   UltimaAtualizacao = agora,
                                   Parcelas = new List<IParcela>()
                               };
            /*
            foreach (var item in _parcela.GetNew(contrato, _settings.SubItens.Take()))
                contrato.Parcelas.Add(item);
            */
            return contrato;
        }

        public List<T> GetNew(int quantity)
        {
            var contratos = new BlockingCollection<T>(quantity);

            Parallel.For(0, quantity, i => contratos.Add(GetNew()));

            return new List<T>(contratos);
        }

        public ICollection<T> GetData(int quantity)
        {
            return GetNew(quantity);
        }
    }
}