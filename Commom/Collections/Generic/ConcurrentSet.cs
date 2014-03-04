using System.Collections.Generic;

namespace Utilities.Common.Collections.Generic
{

    /// <summary>
    /// Código obtido de Dzmitry Huba
    /// </summary>
    /// <see cref="http://dzmitryhuba.blogspot.com.br/2011/04/concurrent-set-based-on-sorted-singly.html"/>
    /// <typeparam name="T"></typeparam>
    public class ConcurrentSet<T>
    {
        private readonly IComparer<T> _mComparer;
        // Sentinel nodes
        private readonly Node _mHead;
        private readonly Node _mTail;

        public ConcurrentSet(IComparer<T> comparer)
        {
            _mComparer = comparer;

            // Sentinel nodes cannot be removed from the
            // list and logically contain negative and
            // positive infinity values from T
            _mTail = new Node(default(T));
            _mHead = new Node(default(T), _mTail);
        }

        // Adds item to the set if no such item
        // exists
        public bool Add(T item)
        {
            // Continue attempting until succeeded
            // or failed
            while (true)
            {
                Node pred, curr;
                // Find where new node must be added
                Find(item, out pred, out curr);
                // Locks nodes starting from predecessor
                // to synchronize concurrent access
                lock (pred)
                {
                    lock (curr)
                    {
                        // Check if found nodes still
                        // meet expectations
                        if (!Validate(pred, curr))
                            continue;
                        // If the value is already in the
                        // set we are done
                        if (Equal(curr, item))
                            return false;
                        // Otherwise add new node
                        var node = new Node(item, curr);
                        // At this point new node becomes
                        // reachable
                        pred.MNext = node;
                        return true;
                    }
                }
            }
        }

        // Removes item from the list if such item
        // exists
        public bool Remove(T item)
        {
            // Continue attempting until succeeded
            // or failed
            while (true)
            {
                Node pred, curr;

                // Find node that must be removed and
                // its predecessor
                Find(item, out pred, out curr);
                // Locks nodes starting from predecessor
                // to synchronize concurrent access
                lock (pred)
                {
                    lock (curr)
                    {
                        // Check if found nodes still
                        // meet expectations
                        if (!Validate(pred, curr))
                            continue;
                        // If the value is not in the set
                        // we are done
                        if (!Equal(curr, item))
                            return false;
                        // Otherwise mark node as removed
                        curr.MRemoved = true;
                        // And make it unreachable
                        pred.MNext = curr.MNext;
                        return true;
                    }
                }
            }
        }

        // Checks if given item exists in the list
        public bool Contains(T item)
        {
            Node pred, curr;
            Find(item, out pred, out curr);

            return !curr.MRemoved && Equal(curr, item);
        }

        // Searches for pair consequent nodes such that
        // curr node contains a value equal or greater
        // than given item
        void Find(T item, out Node pred, out Node curr)
        {
            // Traverse the list without locks as removed
            // nodes still point to other nodes
            pred = _mHead;
            curr = _mHead.MNext;
            while (Less(curr, item))
            {
                pred = curr;
                curr = curr.MNext;
            }
        }

        static bool Validate(Node pred, Node curr)
        {
            // Validate that pair of nodes previously
            // found still meets the expectations
            // which essentially is checking whether
            // nodes still point to each other and no one
            // was removed from the list
            return !pred.MRemoved &&
                   !curr.MRemoved &&
                   pred.MNext == curr;
        }

        bool Less(Node node, T item)
        {
            return node != _mTail &&
                   _mComparer.Compare(node.MValue, item) < 0;
        }

        bool Equal(Node node, T item)
        {
            return node != _mTail &&
                   _mComparer.Compare(node.MValue, item) == 0;
        }

        class Node
        {
            internal readonly T MValue;
            internal volatile Node MNext;
            internal volatile bool MRemoved;

            internal Node(T value, Node next = null)
            {
                MValue = value;
                MNext = next;
            }
        }
    }
}