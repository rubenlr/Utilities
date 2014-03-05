namespace Utilities.DataAccess.Interface
{
    public interface IUpdatable<in T>
    {
        void Update(T entity);
    }
}