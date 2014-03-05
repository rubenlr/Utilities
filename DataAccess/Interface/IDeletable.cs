namespace Utilities.DataAccess.Interface
{
    public interface IDeletable<in T>
    {
        void Delete(T item);
    }
}