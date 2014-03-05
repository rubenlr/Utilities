using System;
using System.Configuration;
using Utilities.Common.Settings;
using Utilities.DataAccess.Exception;

namespace Utilities.DataAccess
{
    public abstract class ConectionStringProvider : IConnectionStringProvider
    {
        private readonly IAppConfig _appConfig;
        private readonly string _sigla;

        protected ConectionStringProvider(IAppConfig appConfig, string sigla)
        {
            _appConfig = appConfig;
            _sigla = sigla;
        }

        public string DataSource
        {
            get
            {
                var config = _appConfig.GetString(_sigla + "-" + Environment.MachineName);

                if (!string.IsNullOrEmpty(config))
                    return config;

                var entityConfig = ConfigurationManager.ConnectionStrings["EntityFrameworkContext"];

                if (entityConfig == null)
                    throw new DataAcessException("Não existe configuração de conexão do banco de dados cadastrada.");

                return entityConfig.ConnectionString;
            }
        }
    }
}
