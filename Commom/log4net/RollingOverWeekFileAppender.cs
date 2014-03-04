using System;
using log4net.Appender;

namespace Utilities.Common.log4net
{
    public class RollingOverWeekFileAppender : RollingFileAppender
    {
        private DateTime _nextWeekendDate;

        public RollingOverWeekFileAppender()
        {
            CalcNextWeekend(DateTime.Now);
        }

        private void CalcNextWeekend(DateTime time)
        {
            // Calc next sunday
            time = time.AddMilliseconds(-time.Millisecond);
            time = time.AddSeconds(-time.Second);
            time = time.AddMinutes(-time.Minute);
            time = time.AddHours(-time.Hour);
            _nextWeekendDate = time.AddDays(7 - (int)time.DayOfWeek);
        }

        protected override void AdjustFileBeforeAppend()
        {
            var now = DateTime.Now;

            if (now >= _nextWeekendDate)
            {
                CalcNextWeekend(now);
                // As you included the day and month AdjustFileBeforeAppend takes care of creating 
                // new file with the new name
                base.AdjustFileBeforeAppend();
            }
        }
    }
}