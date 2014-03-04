using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConectaSolutions.Utils.Parser
{
    public class LineValues<T> : LineValues
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public LineValues(ICollection<FieldValue> fields, string line)
            : base(fields, line, typeof(T))
        {
        }

        public string GetString(T field)
        {
            return GetString(field);
        }

        public int GetInt32(T field)
        {
            return GetInt32(field);
        }

        public double GetDouble(T field)
        {
            return GetDouble(field);
        }

        public DateTime GetDateTime(T field, string format)
        {
            return GetDateTime(field, format);
        }

        public TimeSpan GetTime(T field, string format)
        {
            return GetTime(field, format);
        }

        public decimal GetDecimal(T field)
        {
            return GetDecimal(field);
        }
    }

    public abstract class LineValues
    {
        private static readonly CultureInfo Culture = new CultureInfo("pt-br");
        private readonly Dictionary<string, FieldValue> _fieldsDictionary;

        private LineValues(ICollection<FieldValue> fields, Type lineStyle)
        {
            LineType = lineStyle;

            _fieldsDictionary = new Dictionary<string, FieldValue>(fields.Count());

            foreach (var fieldValue in fields)
                _fieldsDictionary.Add(fieldValue.FieldName, fieldValue);
        }

        protected LineValues(ICollection<FieldValue> fields, string line, Type lineStyle)
            : this(fields, lineStyle)
        {
            LineText = line;
        }

        public Type LineType { get; private set; }
        public string LineText { get; private set; }

        protected string GetString<T>(T field)
        {
            return _fieldsDictionary[field.ToString()].GetString;
        }

        protected int GetInt32<T>(T field)
        {
            return _fieldsDictionary[field.ToString()].GetInt32;
        }

        protected double GetDouble<T>(T field)
        {
            return _fieldsDictionary[field.ToString()].GetDouble;
        }

        protected DateTime GetDateTime<T>(T field, string format)
        {
            return DateTime.ParseExact(GetString(field), format, Culture);
        }

        protected TimeSpan GetTime<T>(T field, string format)
        {
            return TimeSpan.ParseExact(GetString(field), format, Culture);
        }

        protected decimal GetDecimal<T>(T field)
        {
            return _fieldsDictionary[field.ToString()].GetDecimal;
        }

        public LineValues<T> Value<T>()
            where T : struct, IComparable, IConvertible, IFormattable
        {
            return (LineValues<T>) this;
        }

        #region public gettypes
        
        //public string GetString<T>(T field)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetString(field);
        //}

        //public int GetInt32<T>(T field)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetInt32(field);
        //}

        //public double GetDouble<T>(T field)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetDouble(field);
        //}

        //public DateTime GetDateTime<T>(T field, string format)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetDateTime(field, format);
        //}

        //public TimeSpan GetTime<T>(T field, string format)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetTime(field, format);
        //}

        //public decimal GetDecimal<T>(T field)
        //    where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    return Value<T>().GetDecimal(field);
        //}

        #endregion
    }
}
