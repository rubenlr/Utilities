using System.Collections.Generic;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public interface IDataGenerationProvider<T>
    {
        ICollection<T> GetData(int quantity);
    }
}
