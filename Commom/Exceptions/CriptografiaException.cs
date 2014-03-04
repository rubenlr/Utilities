using System;

namespace Utilities.Common.Exceptions
{
    public class CriptografiaException : S1MainException
    {
        public CriptografiaException(string message)
        {
        }

        public CriptografiaException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}