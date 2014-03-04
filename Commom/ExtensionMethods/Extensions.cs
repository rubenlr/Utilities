using System.Linq;

namespace Utilities.Common.ExtensionMethods
{
    public static class Extensions
    {
        public static bool Compare(this byte[] primeiroArray, byte[] segundoArray)
        {
            if (primeiroArray.Length != segundoArray.Length)
                return false;

            return !primeiroArray.Where((t, i) => t != segundoArray[i]).Any();
        }
    }
}
