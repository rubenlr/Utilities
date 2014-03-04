using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using log4net;

namespace Utilities.Common.Exceptions
{
    public class S1MainException : Exception
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public List<Exception> InnerExceptions { private set; get; }

        public S1MainException()
        {
            InnerExceptions = new List<Exception>();
            Log.Error("Erro identificado pelo sistema.", this);
        }

        public S1MainException(IEnumerable<Exception> innerExceptions)
            : this()
        {
            InnerExceptions.AddRange(innerExceptions);
        }

        public S1MainException(string message)
            : base(message)
        { }

        public S1MainException(string format, params object[] args)
            : this(string.Format(format, args))
        { }

        public S1MainException(Exception innerException, string format, params object[] args)
            : this(string.Format(format, args), innerException)
        { }

        [SecuritySafeCritical]
        protected S1MainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            InnerExceptions = new List<Exception>();
        }

        public S1MainException(string message, Exception innerException)
            : base(message, innerException)
        {
            InnerExceptions = new List<Exception>();
        }
    }
}
