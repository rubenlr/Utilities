using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Utilities.DataAccess.Ado
{
    public static class SqlParameterUtil
    {
        private static SqlParameter GetNullableParamters<T>(string field, T data)
            where T : class
        {
            return data == null ? new SqlParameter(field, DBNull.Value)
                                : new SqlParameter(field, data);
        }

        public static SqlParameter[] GetNullableParamters(Dictionary<string, object> paramters)
        {
            return paramters.Select(param => GetNullableParamters(param.Key, param.Value)).ToArray();
        }
    }
}
