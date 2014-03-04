using System;

namespace Utilities.Common.Performance.Mensure
{
    public class LogPerformance
    {
        private readonly string _tipo;
        public DateTime Inicio { get; set; }
        public long Duracao { get; set; }
        public bool Erro { get; set; }
        public string MensagemErro { get; set; }
        public int QuantidadeRetorno { get; set; }

        public LogPerformance(string tipo)
        {
            _tipo = tipo;
            Inicio = DateTime.Now;
            Erro = false;
            MensagemErro = string.Empty;
            QuantidadeRetorno = 0;
        }

        public override string ToString()
        {
            return string.Format("{0:d/M/yyyy HH:mm:ss.fff};{1};{2};{3};{4};{5}", Inicio, _tipo, Duracao, QuantidadeRetorno, Erro, MensagemErro);
        }
    }
}