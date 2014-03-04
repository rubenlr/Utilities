using System;
using System.Diagnostics;
using log4net;

namespace Utilities.Common.Performance.Mensure
{
    public sealed class CronometroPerformance : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger("performance");
        private readonly Stopwatch _cronometro;
        private readonly LogPerformance _logPerformance;

        #region Construtores

        public CronometroPerformance(LogPerformance logPerformance)
        {
            _logPerformance = logPerformance;
            _cronometro = Stopwatch.StartNew();
        }

        public CronometroPerformance(string tipo)
            : this(new LogPerformance(tipo))
        {
        }

        #endregion

        public string MensagemErro
        {
            set
            {
                _logPerformance.MensagemErro = value;
                _logPerformance.Erro = true;
            }
        }

        public int QuantidadeRetorno { set { _logPerformance.QuantidadeRetorno = value; } }

        public void Finalizar()
        {
            _cronometro.Stop();
            _logPerformance.Duracao = _cronometro.ElapsedMilliseconds;

            Log.Info(_logPerformance.ToString());
        }

        public void Dispose()
        {
            Finalizar();
        }
    }
}