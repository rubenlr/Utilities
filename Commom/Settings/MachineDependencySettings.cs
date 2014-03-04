using System;

namespace Utilities.Common.Settings
{
    public class MachineDependencySettings
    {
        private readonly IAppConfig _appConfig;

        public MachineDependencySettings(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public string GetString(string prefixo, string padrao = null)
        {
            var resultado = _appConfig.GetString(string.Format("{0}-{1}", prefixo, Environment.MachineName))
                         ?? _appConfig.GetString(string.Format("{0}", prefixo));

            if (resultado != null || padrao != null)
                return resultado ?? padrao;

            throw new SettingsUtilException("Não foi possível obter configuração de máquina para o prefixo {0}", prefixo);
        }
    }
}