using System;

namespace Utilities.Common.Collections.Generic
{
    public interface IExpiredTimeSettings
    {
        TimeSpan ExpirtedPeriod { get; }
    }
}