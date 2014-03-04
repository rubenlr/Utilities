using System.Collections.Generic;
using System.Threading;

namespace Utilities.Common.Collections.Generic
{
    public class BlockCollectionNotifier<T> : BlockCollection<T>
    {
        private readonly AutoResetEvent _hasItems = new AutoResetEvent(false);

        public BlockCollectionNotifier()
        { }

        public BlockCollectionNotifier(int capacity)
            : base(capacity)
        { }

        public BlockCollectionNotifier(ICollection<T> collection)
        {
            AddRange(collection);
        }

        public override void Add(T item)
        {
            _hasItems.Set();
            base.Add(item);
        }

        public void WaitOne()
        {
            while (Count == 0)
                _hasItems.WaitOne(1000);
        }

        public void WaitOne(int milisecondsTimeout)
        {
            _hasItems.WaitOne(milisecondsTimeout);
        }

        public void ForcesReleaseWhenEmpty()
        {
            if (Count == 0)
                _hasItems.Set();
        }
    }
}
