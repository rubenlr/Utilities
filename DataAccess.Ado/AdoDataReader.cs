using System;
using System.Data.SqlClient;

namespace Utilities.DataAccess.Ado
{
    public class AdoDataReader
    {
        private readonly SqlDataReader _reader;

        public AdoDataReader(SqlDataReader reader)
        {
            _reader = reader;
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public bool HasRows
        {
            get { return _reader.HasRows; }
        }

        public decimal GetDecimal(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetDecimal(ordinal);
        }

        public DateTime GetDateTime(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetDateTime(ordinal);
        }

        public DateTime? GetDateTimeNulable(string column)
        {
            var ordinal = _reader.GetOrdinal(column);

            return !_reader.IsDBNull(ordinal) ? (DateTime?)_reader.GetDateTime(ordinal) : null;
        }

        public double GetDouble(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetDouble(ordinal);
        }

        public Guid GetGuid(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetGuid(ordinal);
        }

        public bool GetBoolean (string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetBoolean(ordinal);
        }

        public Guid? GetGuidNullable(string column)
        {
            var ordinal = _reader.GetOrdinal(column);

            return !_reader.IsDBNull(ordinal) ? (Guid?)_reader.GetGuid(ordinal) : null;
        }

        public int GetInt32(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetInt32(ordinal);
        }

        public int? GetInt32Nullable(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return !_reader.IsDBNull(ordinal) ? (int?)_reader.GetInt32(ordinal) : null;
        }

        public long GetLong(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return _reader.GetInt64(ordinal);
        }

        public string GetString(string column)
        {
            var ordinal = _reader.GetOrdinal(column);
            return !_reader.IsDBNull(ordinal) ? _reader.GetString(ordinal) : null;
        }
    }
}