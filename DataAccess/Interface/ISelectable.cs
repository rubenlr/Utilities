using System;

namespace Utilities.DataAccess.Interface
{
    public interface ISelectable<out T>
        where T : IIdentificable
    {
        T Select(Guid key);
    }
}