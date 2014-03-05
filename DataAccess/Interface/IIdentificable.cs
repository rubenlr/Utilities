using System;

namespace Utilities.DataAccess.Interface
{
    public interface IIdentificable
    {
        Guid Id { set; get; }
    }
}