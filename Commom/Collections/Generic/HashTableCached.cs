using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Utilities.Common.Collections.Generic
{
    public class HashTableCached<THash, TValue>
    {
        private readonly Stopwatch _elapsedTime = new Stopwatch();
        private readonly Dictionary<THash, ValueCacheControl> _dictionary = new Dictionary<THash, ValueCacheControl>();
        private readonly TimeSpan _cachedLimit;
        private readonly int _maxSize = -1;

        private class ValueCacheControl
        {
            public ValueCacheControl(TimeSpan now, TValue valor)
            {
                Criado = now;
                Valor = valor;
            }

            public TValue Valor { private set; get; }
            public TimeSpan Criado { private set; get; }
        }

        private void IniciarTimer()
        {
            if (!_elapsedTime.IsRunning)
                _elapsedTime.Start();
        }

        public HashTableCached(TimeSpan cached)
        {
            IniciarTimer();

            _cachedLimit = cached;
        }

        public HashTableCached(TimeSpan cached, int maxSize)
            : this(cached)
        {
            IniciarTimer();

            _maxSize = maxSize;
        }

        public HashTableCached()
            : this(new TimeSpan(24, 0, 0))
        {
        }

        private void LimparExcedente()
        {
            if (_maxSize >= 0 && Count > _maxSize)
            {
                var hashsExcedentes = new List<THash>(_maxSize);

                lock (this)
                {
                    var excedentes = _dictionary.OrderBy(x => x.Value.Criado).ToList();

                    for (var pos = 0; pos < _maxSize; pos++)
                        hashsExcedentes.Add(excedentes[pos].Key);
                }

                foreach (var hash in hashsExcedentes.Where(ContainsKey))
                {
                    Remove(hash);
                }
            }
        }

        private bool Desatualizado(ValueCacheControl valor)
        {
            return (_elapsedTime.Elapsed - valor.Criado) > _cachedLimit;
        }

        public void Add(THash key, TValue value)
        {
            lock (this)
                _dictionary.Add(key, new ValueCacheControl(_cachedLimit, value));
        }

        public bool ContainsKey(THash key)
        {
            RemoveHashExpirados(key);

            return _dictionary.ContainsKey(key);
        }

        private void RemoveHashExpirados(THash key)
        {
            bool res;

            lock (this)
                res = _dictionary.ContainsKey(key);

            if (res && Desatualizado(_dictionary[key]))
                Remove(key);
        }

        public ICollection<THash> Keys
        {
            get
            {
                lock (this)
                    return _dictionary.Keys;
            }
        }

        public bool Remove(THash key)
        {
            lock (this)
                return _dictionary.Remove(key);
        }

        public TValue this[THash key]
        {
            get
            {
                RemoveHashExpirados(key);

                lock (this)
                {
                    if (ContainsKey(key))
                        return _dictionary[key].Valor;
                }

                return default(TValue);
            }
            set
            {
                Remove(key);

                lock (this)
                    _dictionary[key] = new ValueCacheControl(_cachedLimit, value);
            }
        }

        public void Add(KeyValuePair<THash, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            lock (this)
                _dictionary.Clear();
        }

        public int Count
        {
            get
            {
                lock (this)
                    return _dictionary.Count;
            }
        }
    }
}
