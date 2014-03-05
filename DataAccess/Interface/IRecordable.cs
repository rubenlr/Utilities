namespace Utilities.DataAccess.Interface
{
    public interface IRecordable<in T> : IUpdatable<T>, IInsertable<T>
    {
        void SaveOrUpdate(T item);
    }
}