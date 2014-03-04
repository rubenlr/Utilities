using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Utilities.Common.DataTypes;
using Utilities.Common.Date;
using Utilities.Common.Performance.DataGeneration.Models;
using Utilities.Common.Performance.Mensure;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class SelectStressRunner<T, TKParcela, TKCobranca> : IDisposable
        where T : class, IContrato
        where TKParcela : class, IParcela
        where TKCobranca : class, ICobranca
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ISelectionStressable<T> _select;

        private readonly StressTestDataCollection<T> _contratos;
        private readonly StressTestDataCollection<Periodo> _contratosPeriodo;

        private readonly Random _rand = new Random(123);

        public SelectStressRunner(ISelectionStressable<T> select, StressTestDataCollection<T> data)
        {
            _select = select;

            _contratos = data;

            _contratosPeriodo = new StressTestDataCollection<Periodo>(GetIntervalos(_contratos));
        }

        private IEnumerable<Periodo> GetIntervalos(IEnumerable<IDataAtualizacao> data)
        {
            return data.Select(x => new Periodo
                                        {
                                            Begin = x.UltimaAtualizacao.AddSeconds(-_rand.Next(30) - 1).WithMilliseconds(0),
                                            End = x.UltimaAtualizacao.AddSeconds(_rand.Next(30)).WithMilliseconds(0)
                                        });
        }

        private TaskManagerConcurrent _contratoTask;
        private TaskManagerConcurrent _contratoPeriodoTask;

        public SelectStressRunner<T, TKParcela, TKCobranca> Run()
        {
            _contratoTask = new TaskManagerConcurrent(SelectContrato);
            _contratoPeriodoTask = new TaskManagerConcurrent(SelectContratoPeriodo);

            _contratoTask.Start(_contratos.Count / 50);
            _contratoPeriodoTask.Start(_contratosPeriodo.Count);

            return this;
        }

        public SelectStressRunner<T, TKParcela, TKCobranca> WaitAll()
        {
            _contratoTask.WaitAll();
            _contratoPeriodoTask.WaitAll();

            return this;
        }

        private void SelectContrato()
        {
            var items = _contratos.Take(50);

            using (var cron = new CronometroPerformance("select-unitario-sem-insert"))
            {
                foreach (var item in items)
                {
                    try
                    {
                        var selected = _select.Select(item);

                        if (selected == null || !selected.Id.Equals(item.Id))
                            cron.MensagemErro = "itens diferentes";
                    }
                    catch (Exception ex)
                    {
                        cron.MensagemErro = ex.Message;
                    }
                }
            }
        }

        private void SelectContratoPeriodo()
        {
            var item = _contratosPeriodo.Take();

            using (var cron = new CronometroPerformance("select-periodo-sem-insert"))
            {
                try
                {
                    var itens = _select.Select(item.Begin, item.End);

                    if (itens == null || !itens.Any())
                        cron.MensagemErro = "não há itens!";
                    else
                    {
                        cron.QuantidadeRetorno = itens.Count();
                    }
                }
                catch (Exception ex)
                {
                    cron.MensagemErro = ex.Message;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_contratos != null)
                    _contratos.Dispose();
               
                if (_contratosPeriodo != null)
                    _contratosPeriodo.Dispose();
            }
        }
    }
}
