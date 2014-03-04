using System;
using log4net;

namespace ConectaSolutions.Utils
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public class LogWritter
    {
        private readonly ILog _log;
        
        public LogWritter(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Write(string message, Exception ex)
        {
            WriteFormat(LogLevel.Error, "{0}: {1}{2}", message, ex.Message, ex.StackTrace);
        }

        public void Write(LogLevel level, object message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Info:
                    _log.Info(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        public void WriteFormat(Exception ex, string format, params object[] args)
        {
            Write(string.Format(format, args), ex);
        }

        public void WriteFormat(LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.DebugFormat(format, args);
                    break;
                case LogLevel.Info:
                    _log.InfoFormat(format, args);
                    break;
                case LogLevel.Warning:
                    _log.WarnFormat(format, args);
                    break;
                case LogLevel.Error:
                    _log.ErrorFormat(format, args);
                    break;
                case LogLevel.Fatal:
                    _log.FatalFormat(format, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}
