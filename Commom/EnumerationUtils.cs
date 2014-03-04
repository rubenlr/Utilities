using System;
using System.ComponentModel;
using System.Linq;

namespace Utilities.Common
{
    public static class EnumerationUtils
    {
        public static string GetName(this Enum obj)
        {
            return Enum.GetName(obj.GetType(), obj);
        }

        public static T GetEnum<T>(this string obj)
        {
            return (T)Enum.Parse(typeof(T), obj, true);
        }

        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static string GetEnumDescription(this Enum value, int index)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[index].Description;

            return value.ToString();
        }

        public static T GetEnumFromDescription<T>(this string obj)
        {
            string retorno = string.Empty;

            foreach (string name in Enum.GetNames(typeof(T)))
            {
                var fi = Enum.Parse(typeof(T), name, true).GetType().GetField(name.ToString());
                var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);

                if (attributes.Length > 0 && attributes[0].Description == obj)
                {
                    retorno = name;
                    break;
                }
            }
            return (T)Enum.Parse(typeof(T), retorno, true);
        }

        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetType()
                                 .GetField(value.ToString())
                                 .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}