using System;

namespace Utilities.Common.Date
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
