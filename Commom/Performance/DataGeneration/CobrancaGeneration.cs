using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Common.Performance.DataGeneration.Models;

namespace Utilities.Common.Performance.DataGeneration
{
    public class CobrancaGeneration<T>
        where T : ICobranca, new()
    {
        private readonly Random _rand = new Random(1);

        private T GetNew(IParcela parcela)
        {
            return new T
            {
                Id = Guid.NewGuid(),
                Valor = DateTime.Now.Millisecond,
                UltimaAtualizacao = DateTime.Now.AddSeconds(-_rand.Next(120))
            };
        }

        public List<T> GetNew(IParcela parcela, int qtd)
        {
            var parcelas = new BlockingCollection<T>(qtd);

            Parallel.For(0, qtd, i => parcelas.Add(GetNew(parcela)));

            return new List<T>(parcelas);
        }
    }
}