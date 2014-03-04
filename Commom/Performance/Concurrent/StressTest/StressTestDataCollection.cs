using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public class StressTestDataCollection<T> : BlockingCollection<T>
    {
        public StressTestDataCollection()
        {
        }

        public StressTestDataCollection(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public ICollection<T> Take(int qtd)
        {
            var items = new Collection<T>();

            for (var i = 0; i < qtd && Count > 0; i++)
                items.Add(Take());

            return items;
        }

        private readonly Random _random = new Random(995);

        public ICollection<T> TakeRandomWithoutRemove(int qtd)
        {
            var posicoes = new HashSet<int>();

            if (qtd >= Count)
                throw new Exception("Não há itens suficientes para o teste");

            while (posicoes.Count < qtd)
            {
                while (!posicoes.Add(_random.Next(Count - 1)))
                {
                    // Se der erro continua no loop. sem corpo.
                }
            }

            var itens = ToArray();
            var result = new List<T>(qtd);

            var posicoesQueue = new Queue<int>(posicoes);

            while (posicoesQueue.Count > 0)
                result.Add(itens[posicoesQueue.Dequeue()]);

            return result;
        }
    }
}
