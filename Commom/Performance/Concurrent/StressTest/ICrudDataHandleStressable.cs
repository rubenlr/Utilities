namespace Utilities.Common.Performance.Concurrent.StressTest
{
    public interface ICrudDataHandleStressable<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        T Select(T item);
    }
}
