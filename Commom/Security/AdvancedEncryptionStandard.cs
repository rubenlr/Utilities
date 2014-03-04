using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Utilities.Common.Exceptions;

namespace Utilities.Common.Security
{
    static class AdvancedEncryptionStandard
    {
        public enum AesCryptographyLevel
        {
            Aes128 = 128,
            Aes192 = 192,
            Aes256 = 256
        }

        #region Private

        private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();

                Rijndael alg = null;
                try
                {
                    alg = Rijndael.Create();

                    alg.Key = key;
                    alg.IV = iv;


                    CryptoStream cs = null;
                    try
                    {
                        cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

                        var result = ms;
                        ms = null;

                        cs.Write(clearData, 0, clearData.Length);
                        cs.Close();

                        cs = null;

                        return result.ToArray();
                    }
                    finally
                    {
                        if (cs != null)
                            cs.Dispose();
                    }
                }
                finally
                {
                    if (alg != null)
                        alg.Dispose();
                }
            }
            finally
            {
                if (ms != null)
                    ms.Dispose();
            }
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream();
                Rijndael alg = null;
                try
                {
                    alg = Rijndael.Create();

                    alg.Key = key;
                    alg.IV = iv;

                    CryptoStream cs = null;
                    try
                    {
                        cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

                        var result = ms;
                        ms = null;

                        cs.Write(cipherData, 0, cipherData.Length);
                        cs.Close();

                        cs = null;

                        return result.ToArray();
                    }
                    finally
                    {
                        if (cs != null)
                            cs.Dispose();
                    }
                }
                finally
                {
                    if (alg != null)
                        alg.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Não foi possível efetuar a descriptografia.", ex);
            }
            finally
            {
                if (ms != null)
                    ms.Dispose();
            }
        }

        #endregion

        #region Public

        public static string Encrypt(string data, byte[] key, byte[] salt, AesCryptographyLevel bits)
        {
            var clearBytes = Encoding.Unicode.GetBytes(data);

            var pdb = new Rfc2898DeriveBytes(key, salt, 5);

            switch (bits)
            {
                case AesCryptographyLevel.Aes128:
                    return Convert.ToBase64String(Encrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16)));
                case AesCryptographyLevel.Aes192:
                    return Convert.ToBase64String(Encrypt(clearBytes, pdb.GetBytes(24), pdb.GetBytes(16)));
                default:
                    return Convert.ToBase64String(Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16)));
            }
        }

        public static string Decrypt(string data, byte[] key, byte[] salt, AesCryptographyLevel bits)
        {
            var cipherBytes = Convert.FromBase64String(data);

            var pdb = new Rfc2898DeriveBytes(key, salt, 5);

            switch (bits)
            {
                case AesCryptographyLevel.Aes128:
                    return Encoding.Unicode.GetString(Decrypt(cipherBytes, pdb.GetBytes(16), pdb.GetBytes(16)));
                case AesCryptographyLevel.Aes192:
                    return Encoding.Unicode.GetString(Decrypt(cipherBytes, pdb.GetBytes(24), pdb.GetBytes(16)));
                default:
                    return Encoding.Unicode.GetString(Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16)));
            }
        }

        #endregion
    }
}