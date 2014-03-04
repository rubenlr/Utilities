using System;

namespace Utilities.Common.Parser
{
    public class FieldValue
    {
        private readonly string _fieldname;
        private readonly string _value;

        public FieldValue(string fieldName, string value)
        {
            _fieldname = fieldName;
            _value = value;
        }

        public string FieldName
        {
            get { return _fieldname; }
        }

        public string GetString
        {
            get { return _value.Trim(); }
        }

        public int GetInt32
        {
            get { return Convert.ToInt32(_value); }
        }

        public double GetDouble
        {
            get { return Convert.ToDouble(_value) / 100; }
        }

        public decimal GetDecimal
        {
            get { return Convert.ToDecimal(_value) / 100; }
        }
    }
}