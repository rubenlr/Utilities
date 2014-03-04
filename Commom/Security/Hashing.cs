using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Utilities.Common.Security
{
    public class Hashing : IHashing
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public Hashing(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
            _hashAlgorithm.Initialize();
        }

        public byte[] ComputeHash(byte[] password, byte[] salt)
        {
            return _hashAlgorithm.ComputeHash(new Rfc2898DeriveBytes(password, salt, 5).GetBytes(50));
        }

        public byte[] ComputeHash(object obj)
        {
            return _hashAlgorithm.ComputeHash(ObjectToByteArray(obj));
        }

        public string ComputeHash(string data)
        {
            var bytes = new UTF8Encoding().GetBytes(data);
            var hash = _hashAlgorithm.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        private static readonly Object Locker = new Object();
        private static byte[] ObjectToByteArray(Object objectToSerialize)
        {
            var fs = new MemoryStream();
            var formatter = new BinaryFormatter();
            try
            {
                //Here's the core functionality! One Line!
                //To be thread-safe we lock the object
                lock (Locker)
                {
                    formatter.Serialize(fs, objectToSerialize);
                }
                return fs.ToArray();
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Error occurred during serialization. Message: " +
                se.Message);
                return null;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
