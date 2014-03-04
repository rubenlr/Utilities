using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utilities.Common.Performance.DataGeneration
{
    public class PoolGeneration<T>
    {
        readonly ConcurrentQueue<T> _queue;
        readonly Dictionary<T, int> _items;

        public PoolGeneration(Dictionary<T, int> items)
        {
            _queue = new ConcurrentQueue<T>();
            _items = items;
        }

        private void Generate()
        {
            foreach (var keyValuePair in _items)
                for (var i = 0; i < keyValuePair.Value; i++)
                    _queue.Enqueue(keyValuePair.Key);
        }

        public T Take()
        {
            if (_queue.Count == 0)
                Generate();

            var obj = default(T);
            var dequeued = false;

            while (!dequeued)
                dequeued = _queue.TryDequeue(out obj);

            return obj;
        }
    }
}