using System;
using System.Collections.Generic;

namespace Utilities.Common.Performance.DataGeneration.Models
{
    public interface IParcela : IDataAtualizacao 
    {
        Guid Id { set; get; }
        decimal Valor { set; get; }
        List<ICobranca> Cobrancas { get; set; }
    }

    [Serializable]
    public class Parcela : IParcela
    {
        public Guid Id { set; get; }
        public decimal Valor { set; get; }
        public DateTime UltimaAtualizacao { get; set; }
        public List<ICobranca> Cobrancas { get; set; }
    }
}
