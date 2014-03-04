using System;

namespace Utilities.Common.Parser
{
    public class FieldDescriptionDelimited<T> : FieldDescription<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public int Position { get; private set; }

        public FieldDescriptionDelimited(T enumerationName, int position) 
            : base(enumerationName)
        {
            Position = position;
        }
    }
}