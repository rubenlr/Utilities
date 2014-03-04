using System;

namespace Utilities.Common.Performance.DataGeneration.Models
{
    public interface ICobranca : IDataAtualizacao
    {
        Guid Id { set; get; }
        decimal Valor { get; set; }
    }

    [Serializable]
    public class Cobranca : ICobranca
    {
        public Guid Id { set; get; }
        public decimal Valor { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
    }
}
