using System;
using System.Data.SqlClient;
using Utilities.DataAccess.Exception;

namespace Utilities.DataAccess.Ado
{
    public class AdoConnection : IDisposable
    {
        private readonly SqlConnection _connection;

        public AdoConnection(string dataSource)
        {
            _connection = new SqlConnection(dataSource);
            _connection.Open();
        }
        
        public AdoConnection(IConnectionStringProvider connectionStringProvider) 
            : this(connectionStringProvider.DataSource)
        {
        }
        
        /// <summary>
        /// Executes a Transact-SQL statement and returns the number of rows affected.
        /// </summary>
        /// <param name="cmdText">Transact-SQL statement</param>
        /// <param name="parameters">Paramters for Transact-SQL</param>
        /// <exception cref="System.Data.SqlClient.SqlException">Ocurred when database cannot execute your query</exception>
        /// <returns>The number of rows affected.</returns>
        private int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            try
            {
                return CreateSqlCommand(cmdText, parameters).ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    throw new DuplicatedKeyException(ex);

                throw;
            }
        }

        private SqlCommand CreateSqlCommand(string cmdText, SqlTransaction transaction, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(cmdText, _connection, transaction);

            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            return command;
        }

        private SqlCommand CreateSqlCommand(string cmdText, params SqlParameter[] parameters)
        {
            return CreateSqlCommand(cmdText, null, parameters);
        }

        public void Insert(string cmdText, params SqlParameter[] parameters)
        {
            var inserted = ExecuteNonQuery(cmdText, parameters);

            if (inserted == 0)
                throw new DataAcessException("Não foi possível inserir os dados. Retornado 0");
        }

        public int Update(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, parameters);
        }

        public int Delete(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, parameters);
        }

        public AdoDataReader Select(string cmdText, params SqlParameter[] parameters)
        {
            var reader = CreateSqlCommand(cmdText, parameters).ExecuteReader();

            return new AdoDataReader(reader);
        }

        public AdoDataReader SelectAll(string cmdText)
        {
            var reader = CreateSqlCommand(cmdText).ExecuteReader();

            return new AdoDataReader(reader);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
