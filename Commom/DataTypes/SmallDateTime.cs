using System;

namespace ConectaSolutions.Utils.DataTypes
{
    public struct SmallDateTime
    {
        private readonly DateTime _value;

        public SmallDateTime(DateTime d)
        {
            _value = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
        }

        public static implicit operator DateTime(SmallDateTime p)
        {
            return p._value;
        }

        public static implicit operator SmallDateTime(DateTime d)
        {
            return new SmallDateTime(d);
        }
    }
}