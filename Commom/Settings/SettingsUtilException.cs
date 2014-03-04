using Utilities.Common.Exceptions;

namespace Utilities.Common.Settings
{
    public class SettingsUtilException : S1MainException
    {
        public SettingsUtilException(string message, params object[] paramters)
            : base(message, paramters)
        {
        }
    }
}