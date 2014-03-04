using System.Collections;
using System.Collections.Generic;

namespace Utilities.Common.Collections.Generic
{
    public class Cache<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly int _maxSize;
        private readonly IDictionary<TKey, TValue> _items;
        private readonly LinkedList<TKey> _accessOrder;

        public Cache(int maxSize)
        {
            _maxSize = maxSize;

            _items = new Dictionary<TKey, TValue>(_maxSize);
            _accessOrder = new LinkedList<TKey>();
        }

        public void Add(TKey key, TValue value)
        {
            if (Count == _maxSize)
                Remove(_accessOrder.First.Value);

            if (ContainsKey(key))
                Remove(key);

            _items.Add(key, value);
            _accessOrder.AddLast(key);
        }

        public bool Remove(TKey key)
        {
            _accessOrder.Remove(key);
            return _items.Remove(key);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    _accessOrder.Remove(key);
                    _accessOrder.AddLast(key);
                }

                return _items[key];
            }
            set
            {
                Add(key, value);
            }
        }

        #region Proxy

        public int Count
        {
            get { return _items.Count; }
        }

        public bool ContainsKey(TKey key)
        {
            return _items.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _items.Keys; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _items.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _items.Values; }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return _items.Contains(keyValuePair);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        #endregion
    }
}
