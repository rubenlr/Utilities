using System.Collections.Generic;

namespace Utilities.Common.Performance.DataGeneration
{
    public class DataGenerationSettings
    {
        public PoolGeneration<int> SubItens = new PoolGeneration<int>(new Dictionary<int, int> { { 1, 60 }, { 2, 50 }, { 3, 45 }, { 4, 30 }, { 5, 20 }, { 6, 10 }, { 7, 5 } });
    }
}
