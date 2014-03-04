
namespace Utilities.Common.Concurrent
{
    public class TypeSyncronized<T> where T : new()
    {
        private T _value;

        public TypeSyncronized()
        {
            _value = default(T);
        }

        public TypeSyncronized(T value)
            : this()
        {
            _value = value;
        }

        public T Value
        {
            set
            {
                lock (this)
                    _value = value;
            }
            get
            {
                lock (this)
                    return _value;
            }
        }
    }
}
