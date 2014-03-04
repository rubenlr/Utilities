using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Utilities.Common
{
    public static class DocumentValidation
    {
        private const int CpfTamanho = 11;
        private const int CnpjTamanho = 14;
        private const string CpfFormat = @"^\d{3}[.]?\d{3}[.]?\d{3}[-]?\d{2}$";
        private const string CnpjFormat = @"^\d{2}[.]?\d{3}[.]?\d{3}[\/]?\d{4}[-]?\d{2}$";

        public static bool ValidarDocumento(string documento, TipoDocumento tipoDocumento)
        {
            string regex;
            int tamanho;

            if (string.IsNullOrWhiteSpace(documento))
                return false;

            if (tipoDocumento.Equals(TipoDocumento.CPF))
            {
                tamanho = CpfTamanho;
                regex = CpfFormat;
            }
            else
            {
                tamanho = CnpjTamanho;
                regex = CnpjFormat;
            }

            if (!Regex.IsMatch(documento, regex))
                return false;

            RemoveNaoNumericos(ref documento);

            var digito = documento.Substring(tamanho - 2, 2);
            documento = documento.Substring(0, tamanho - 2);

            //Calculo do 1º digito
            documento += GerarDigito(documento, tipoDocumento);

            //Calculo do 2º digito
            documento += GerarDigito(documento, tipoDocumento);

            if (digito == documento.Substring(tamanho - 2, 2))
                return true;
            return false;
        }

        private static string GerarDigito(string documento, TipoDocumento tipoDocumento)
        {
            var peso = 2;
            var soma = 0;

            for (var i = documento.Length - 1; i >= 0; i--)
            {
                soma += peso * Convert.ToInt32(documento[i].ToString(CultureInfo.InvariantCulture));
                peso++;

                if ((tipoDocumento == TipoDocumento.CNPJ) && (peso == 10))
                {
                    peso = 2;
                }
            }

            var numero = 11 - (soma % 11);

            if (numero > 9)
                numero = 0;

            return numero.ToString(CultureInfo.InvariantCulture);
        }

        private static void RemoveNaoNumericos(ref string documento)
        {
            documento = Regex.Replace(documento, "[^0-9]", "");
        }
    }
}
