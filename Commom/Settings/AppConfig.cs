using System;
using System.Configuration;
using System.Reflection;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;

namespace Utilities.Common.Settings
{
    public class AppConfig : IAppConfig
    {
        private readonly ILogger _log;

        public AppConfig(ILogger log)
        {
            _log = log;
        }

        public AppConfig()
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            var kernel = new StandardKernel(settings, new INinjectModule[] { new Log4NetModule() });

            _log = kernel.Get<ILoggerFactory>().GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        private object GetValue(string section)
        {
            try
            {
                ConfigurationManager.RefreshSection(section);
                return ConfigurationManager.AppSettings[section];
            }
            catch (Exception ex)
            {
                var message = string.Format("Não foi possível recuperar configuração [{0}]", section);
                _log.Error(message, ex);
                throw;
            }
        }

        public string GetString(string section)
        {
            return Convert.ToString(GetValue(section));
        }

        public string GetString(string section, string _default)
        {
            try
            {
                var value = GetValue(section);

                if (value == null)
                {
                    _log.Warn("Usando valor padrão para configuração {0}", section);
                    return _default;
                }

                return Convert.ToString(value);
            }
            catch
            {
                return _default;
            }
        }

        public int GetInt32(string section)
        {
            return Convert.ToInt32(GetValue(section));
        }

        public decimal GetDecimal(string section)
        {
            return Convert.ToDecimal(GetValue(section));
        }

        public Guid GetGuid(string section)
        {
            return new Guid(GetValue(section).ToString());
        }

        public Guid? GetGuidNullable(string section)
        {
            var value = GetValue(section).ToString();

            return !string.IsNullOrEmpty(value) ? new Guid(value) : (Guid?) null;
        }

        public int GetInt32(string section, int _default)
        {
            try
            {
                var value = GetValue(section);

                if (value == null)
                {
                    _log.Warn("Usando valor padrão para configuração {0}", section);
                    return _default;
                }

                return Convert.ToInt32(value);
            }
            catch
            {
                return _default;
            }
        }

        public bool GetBoolean(string section)
        {
            return Convert.ToBoolean(GetValue(section));
        }

        public bool GetBoolean(string section, bool _default)
        {
            try
            {
                var value = GetValue(section);

                if (value == null)
                {
                    _log.Warn("Usando valor padrão para configuração {0}", section);
                    return _default;
                }

                return Convert.ToBoolean(value);
            }
            catch
            {
                return _default;
            }
        }

        public Guid GetGuid(string section, Guid _default)
        {
            try
            {
                var value = GetValue(section);

                if (value != null && Guid.TryParse(value.ToString(), out _default))
                    return _default;
                else
                {
                    _log.Warn("Usando valor padrão para configuração {0}", section);
                    return _default;
                }
            }
            catch
            {
                return _default;
            }
        }
    }
}
