using Utilities.Common.Partners;

namespace Utilities.Common.Math
{
    public class Porcentagem
    {
        private Porcentagem()
        {    
        }

        public static Porcentagem Instance
        {
            get { return Singleton<Porcentagem>.Instance; }
        }

        public double GetQuantidadeProporcional(double total, double totalParcial, double quantidadeParcial)
        {
            return quantidadeParcial*total/totalParcial;
        }
    }
}
