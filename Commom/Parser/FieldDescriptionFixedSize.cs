using System;

namespace Utilities.Common.Parser
{
    public class FieldDescriptionFixedSize<T> : FieldDescription<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public FieldDescriptionFixedSize(T enumerationName, int inicio, int fim)
            : base(enumerationName)
        {
            if (fim < inicio)
                throw new ArgumentException("fim deve ser maior que inicio");

            Inicio = inicio;
            Fim = fim;
        }

        public int Inicio { get; private set; }
        public int Fim { get; private set; }
    }
}
