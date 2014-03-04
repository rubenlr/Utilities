using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utilities.Common.Collections.Generic
{
    public class BlockCollection<T> : IDisposable
    {
        private readonly BlockingCollection<T> _queue;

        public BlockCollection()
        {
            _queue = new BlockingCollection<T>();
        }

        public BlockCollection(int capacity)
        {
            _queue = new BlockingCollection<T>(capacity);
        }

        public virtual void Add(T item)
        {
            _queue.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public T Take()
        {
            return _queue.Take();
        }

        public ICollection<T> TakeAll()
        {
            var items = new List<T>();

            while (Count > 0)
                items.Add(_queue.Take());

            return items;
        }

        public int Count
        {
            get
            {
                return _queue.Count;
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
                if (_queue != null)
                    _queue.Dispose();
            }
        }
    }
}
