namespace Utilities.DataAccess.Interface
{
    public interface IInsertable<in T>
    {
        void Insert(T item);
    }
}