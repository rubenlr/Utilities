using System.Reflection;

namespace Utilities.Common.Partners
{
    public static class Singleton<T> where T : class
    {
        private static object _syncobj = new object();
        private static volatile T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncobj)
                    {
                        if (_instance == null)
                        {
                            const BindingFlags flags = BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic;

                            _instance = typeof(T).InvokeMember(typeof(T).Name, flags, null, null, null) as T;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
