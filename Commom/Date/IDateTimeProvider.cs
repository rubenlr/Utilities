using System;

namespace Utilities.Common.Date
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}