using System;
using System.Collections.Generic;
using Utilities.Common.Performance.DataGeneration.Models;

namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public interface ISelectionStressable<T>
        where T : class, IContrato
    {
        T Select(T item);
        ICollection<T> Select(DateTime begin, DateTime end);
    }
}