using System;
using System.Globalization;

namespace Utilities.Common.Parser
{
    public class FieldDescription
    {
        public FieldDescription(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
    }

    public class FieldDescription<T> : FieldDescription
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public FieldDescription(T enumerationName)
            : base(enumerationName.ToString(CultureInfo.InvariantCulture))
        {
        }
    }
}
