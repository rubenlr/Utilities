using System;

namespace Utilities.Common.Date
{
    public class DateTimeDiff
    {
        public double Year
        {
            get { return End.Year - Begin.Year; }
        }

        public double Month
        {
            get
            {
                var diff = End.Month - Begin.Month;
                var lastDayOfMonth = DateTime.DaysInMonth(End.Year, End.Month) == End.Day;

                if (End.Day < Begin.Day && !lastDayOfMonth)
                    diff -= 1;

                diff += (End.Year - Begin.Year) * 12;

                return diff % 12;
            }
        }

        public double Day
        {
            get
            {
                if (End.Day >= Begin.Day)
                    return End.Day - Begin.Day;

                var mesAnterior = End.AddMonths(-1).WithDayOrLastDayInMonth(Begin.Day);

                return End.Subtract(mesAnterior).TotalDays;
            }
        }

        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }

        public DateTimeDiff(DateTime begin, DateTime end)
        {
            if (begin > end)
                throw new ArgumentException("A data inicial não pode ser maior que a final");

            Begin = begin;
            End = end;
        }
    }
}