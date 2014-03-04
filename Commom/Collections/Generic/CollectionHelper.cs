using System.Collections.Generic;

namespace Utilities.Common.Collections.Generic
{
    public static class CollectionHelper
    {
        public static ICollection<T> Clone<T>(IEnumerable<T> items)
        {
            return new List<T>(items);
        }
    }
}
