using System;
using System.Collections.Generic;

namespace Utilities.Common.Performance.DataGeneration.Models
{
    public interface IContrato : IDataAtualizacao
    {
        Guid Id { get; set; }
        string Nome { get; set; }
        List<IParcela> Parcelas { get; set; }
    }

    public class Contrato : IContrato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public List<IParcela> Parcelas { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Nome: {1}", Id, Nome);
        }

        public bool Equals(Contrato contrato)
        {
            return contrato.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            var contrato = obj as Contrato;

            return contrato != null ? Equals(contrato) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
