using System.Data.SqlClient;

namespace Utilities.DataAccess.Exception
{
    public class DuplicatedKeyException : DataAcessException
    {
        public DuplicatedKeyException(SqlException innerException)
            : base("Chave primária já existe.", innerException)
        { }
    }
}
