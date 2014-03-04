using System;
using System.Globalization;
using System.Linq;

namespace Utilities.Common.Test
{
    public static class Rand
    {
        private static readonly Random Random = new Random();

        public static string String
        {
            get { return Guid.ToString(); }
        }

        public static string String10
        {
            get { return Guid.ToString().Substring(0, 10); }
        }

        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        public static string CartaoCom10
        {
            get { return Random.Next().ToString(CultureInfo.InvariantCulture).PadRight(10, '0'); }
        }

        public static decimal Decimal
        {
            get { return Random.Next(); }
        }

        public static int Int
        {
            get { return Random.Next(); }
        }

        public static DateTime DateTime
        {
            get
            {
                const int totalMinutes = (365 * 60 * 24 * 10);
                var qtdDias = (Int * Int % 2 == 0 ? -1 : 1);
                return DateTime.Now.AddMinutes(qtdDias % totalMinutes);
            }
        }

        public static DateTime Date
        {
            get
            {
                const int totalDias = (365 * 10);
                var qtdDias = (Int * Int % 2 == 0 ? -1 : 1);
                return DateTime.Now.AddDays(qtdDias % totalDias);
            }
        }

        public static string StringMax(int max)
        {
            var str = String;

            while (str.Length < max)
                str += String;

            return str.Substring(0, max);
        }

        public static int IntRandonRange(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static bool Bool
        {
            get { return Int % 2 == 0; }
        }

        public static string Cartao
        {
            get
            {
                var cartao = string.Empty;

                do
                {
                    cartao += Int.ToString(CultureInfo.InvariantCulture);
                } while (cartao.Length < 16);

                return cartao.Substring(0, 16);
            }
        }

        public static T Shuffle<T>(params T[] itens)
        {
            return itens[Int%itens.Count()];
        }
    }
}