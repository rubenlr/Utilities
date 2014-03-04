using System;
using System.Configuration;
using System.Reflection;
using log4net;

namespace Utilities.Common
{
    public static class ConnectionString
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetValue(string section)
        {
            try
            {
                ConfigurationManager.RefreshSection("connectionStrings");
                return ConfigurationManager.ConnectionStrings[section].ConnectionString;
            }
            catch (Exception ex)
            {
                var message = string.Format("Não foi possível recuperar a connectionStrings [{0}]", section);
                Log.Error(message, ex);
                throw;
            }
        }
    }
}