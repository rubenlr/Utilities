using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace Utilities.Common
{
    public static class Converter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Regex NoNumeric = new Regex(@"[^0-9]");
        
        public static byte[] ToByteArray(object bytes)
        {
            return (byte[])bytes;
        }
        
        public static byte[] ToByteArray(Stream inputStream)
        {
            if (inputStream.CanSeek)
                inputStream.Seek(0, SeekOrigin.Begin);

            var output = new byte[inputStream.Length];
            inputStream.Read(output, 0, output.Length);
            return output;
        }

        public static byte[] ToByteArray(string str)
        {
            return new UTF8Encoding().GetBytes(str);
        }

        public static String ToString(byte[] array)
        {
            return new UTF8Encoding().GetString(array);
        }

        public static bool? ToNullableBool(object obj)
        {
            if (obj == null)
                return null;

            return Convert.ToBoolean(obj);
        }

        public static long ToLong(string number)
        {
            var plainNumber = NoNumeric.Replace(number, "");

            return Convert.ToInt64(plainNumber);
        }

        public static long ToLong(object number)
        {
            return ToLong(number.ToString());
        }

        public static int? ToNullableInt(object obj)
        {
            if (obj != DBNull.Value)
                return Convert.ToInt32(obj);

            return null;
        }

        public static DateTime ToDateTime(object obj)
        {
            return Convert.ToDateTime(obj);
        }
        
        public static DateTime ToDateTime(object date, string format)
        {
            if (date == null)
                throw new ArgumentNullException("date");

            try
            {
                return DateTime.ParseExact(date.ToString(), format, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                var message = String.Format("Impossível converter data {0} no formato {1}", date, format);
                Log.Error(message, ex);
                throw;
            }
        }

        public static DateTime? ToNullableDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;

            return ToDateTime(obj);
        }

        public static int ToInt32(string numero)
        {
            return Convert.ToInt32(numero);
        }

        public static string ToJson<T>(this T obj) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();

                serializer.WriteObject(ms, obj);
                byte[] json = ms.ToArray();
                
                ms.Close();
                ms = null;

                return Encoding.UTF8.GetString(json, 0, json.Length);
            }
            finally
            {
                if (ms != null)
                    ms.Dispose();
            }
        }

        public static T FromJson<T>(this T obj, string json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}
