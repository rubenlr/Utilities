using System;

namespace Utilities.Common.Settings
{
    public interface IAppConfig
    {
        string GetString(string section);
        string GetString(string section, string _default);
        int GetInt32(string section);
        int GetInt32(string section, int _default);
        bool GetBoolean(string section);
        bool GetBoolean(string section, bool _default);
        decimal GetDecimal(string section);
        Guid GetGuid(string section, Guid _default);
    }
}