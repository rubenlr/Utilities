using System.Collections;
using System.Collections.Generic;

namespace Utilities.Common.Collections.Generic
{
    public class QueueSynchronized<T>
    {
        private readonly Queue _syncQueue;

        public QueueSynchronized()
        {
            _syncQueue = Queue.Synchronized(new Queue(10));
        }

        public QueueSynchronized(int capacity)
        {
            _syncQueue = Queue.Synchronized(new Queue(capacity));
        }

        public QueueSynchronized(ICollection<T> collection)
        {
            var queue = new Queue(collection.Count);

            foreach (var c in collection)
                queue.Enqueue(c);

            _syncQueue = Queue.Synchronized(queue);
        }

        public void Enqueue(T item)
        {
            lock (this)
                _syncQueue.Enqueue(item);
        }

        public T Dequeue()
        {
            lock (this)
                return (T)_syncQueue.Dequeue();
        }

        public T Peek()
        {
            lock (this)
                return (T)_syncQueue.Peek();
        }

        public int Count
        {
            get
            {
                lock (this)
                    return _syncQueue.Count;
            }
        }
    }
}
