using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Common.DataTypes;

namespace Utilities.Common.Date
{
    public static class DateTimeUtil
    {
        public static DateTime ProximoDiaDaLista(IEnumerable<int> diasDisponiveis, DateTime referencia)
        {
            var existeProximoDia = diasDisponiveis.Any(x => x > referencia.Day);

            if (existeProximoDia)
                return referencia.WithDayOrLastDayInMonth(diasDisponiveis.OrderBy(x => x).First(x => x > referencia.Day));

            return referencia.AddMonths(1).WithDayOrLastDayInMonth(diasDisponiveis.OrderBy(x => x).First());
        }

        public static DateTime? Recuperar(this DateTime? obj)
        {
            if (obj.HasValue)
                return new DateTime(obj.Value.Ticks, DateTimeKind.Local).WithMilliseconds(0);
            return null;
        }

        public static DateTime Recuperar(this DateTime obj)
        {
            return new DateTime(obj.Ticks, DateTimeKind.Local).WithMilliseconds(0);
        }

        public static List<Periodo> RecuperarIntervalos(IEnumerable<DateTime> datas)
        {
            var periodos = new List<Periodo>();

            if (datas != null)
            {
                if (datas.Any())
                {
                    var datasSorted = Sort(datas);

                    var ultimaData = DateTime.MinValue;
                    var periodo = new Periodo { Begin = datasSorted.First() };

                    foreach (var data in datasSorted)
                    {
                        if (ultimaData.AddDays(1) != data && ultimaData != data && ultimaData != DateTime.MinValue)
                        {
                            periodo.End = ultimaData;
                            periodos.Add(periodo);

                            periodo = new Periodo { Begin = data };
                        }

                        ultimaData = data;
                    }

                    periodo.End = ultimaData;
                    periodos.Add(periodo);
                }
            }

            return periodos;
        }

        public static IEnumerable<DateTime> Sort(IEnumerable<DateTime> datas)
        {
            return datas.OrderBy(x => x.Date);
        }

        public static DateTime WithDayOrLastDayInMonth(this DateTime dateTime, int day)
        {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            if (daysInMonth < day)
                day = daysInMonth;

            return new DateTime(dateTime.Year,
                                dateTime.Month,
                                day,
                                dateTime.Hour,
                                dateTime.Minute,
                                dateTime.Second,
                                dateTime.Millisecond);
        }

        public static DateTime WithMilliseconds(this DateTime dateTime, int milliseconds)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, milliseconds);
        }

        public static bool EqualsMock(this DateTime dateTime, DateTime d)
        {
            return dateTime.WithMilliseconds(0).Equals(d.WithMilliseconds(0));
        }

        public static bool EqualsMock(this DateTime? dateTime, DateTime? d)
        {
            if (ReferenceEquals(dateTime, d)) return true;
            if (dateTime == null || d == null) return false;

            return dateTime.Value.WithMilliseconds(0).Equals(d.Value.WithMilliseconds(0));
        }

        public static DateTimeDiff Diff(this DateTime end, DateTime begin)
        {
            return new DateTimeDiff(begin, end);
        }

        public static bool EhFeriadoBancario(this DateTime data)
        {
            if (data.Date.Month == 1 && data.Date.Day == 1)
                return true;
            if (data.Date.Month == 4 && data.Date.Day == 21)
                return true;
            if (data.Date.Month == 5 && data.Date.Day == 1)
                return true;
            if (data.Date.Month == 9 && data.Date.Day == 7)
                return true;
            if (data.Date.Month == 10 && data.Date.Day == 12)
                return true;
            if (data.Date.Month == 11 && data.Date.Day == 2)
                return true;
            if (data.Date.Month == 11 && data.Date.Day == 15)
                return true;
            if (data.Date.Month == 12 && data.Date.Day == 25)
                return true;

            var pascoa = DataPascoa(data.Year);
            if (Equals(pascoa, data.Date))
                return true;

            var carnaval = pascoa.AddDays(-47);
            if (Equals(carnaval, data.Date) || Equals(carnaval.AddDays(-1), data.Date))
                return true;

            var corpusChristi = pascoa.AddDays(60);
            if (Equals(corpusChristi, data.Date))
                return true;

            return false;
        }

        private static DateTime DataPascoa(int year)
        {
            const int x = 24;
            var y = year < 2099 ? 5 : 6;
            var a = year % 19;
            var b = year % 4;
            var c = year % 7;
            var d = (19 * a + x) % 30;
            var e = (2 * b + 4 * c + 6 * d + y) % 7;

            return (d + e) > 9 ? new DateTime(year, 4, (d + e - 9)) : new DateTime(year, 3, d + e + 22);
        }

        public static DateTime AddDaysUtilBancario(this DateTime data, int days)
        {
            var dias = days;

            while (dias != 0)
            {
                data = data.AddDays(days > 0 ? 1 : -1);

                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday && !data.EhFeriadoBancario())
                    dias += days > 0 ? -1 : 1;
            }

            return data;
        }

        public static DateTime DiaUtilBancario(this DateTime data)
        {
            while (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday || data.EhFeriadoBancario())
                data = data.AddDays(1);

            return data;
        }
    }
}
