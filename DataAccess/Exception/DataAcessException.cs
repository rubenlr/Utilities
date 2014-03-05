using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using Utilities.Common.Exceptions;

namespace Utilities.DataAccess.Exception
{
    public class DataAcessException : S1MainException
    {
        public DataAcessException()
        {
        }

        public DataAcessException(IEnumerable<System.Exception> innerExceptions)
            : base(innerExceptions)
        {
        }

        public DataAcessException(string message)
            : base(message)
        { }

        public DataAcessException(string format, params object[] args)
            : this(string.Format(format, args))
        { }

        [SecuritySafeCritical]
        protected DataAcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}

        public DataAcessException(string message, System.Exception innerException)
            : base(message, innerException)
        {}
    }
}