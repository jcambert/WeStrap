using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WeCommon
{
    public static class Extensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => 
            self?.Select((item, index) => (item, index)) ?? new List<(T,int)> ();
        public static TEnum FromDescriptionString<TEnum>(this string s, TEnum @default)
        {
            if (string.IsNullOrEmpty(s)) return @default;
            var fi = from e in typeof(TEnum).GetFields()
                     where (e.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute).Description == s
                     select e;
            var result = fi.FirstOrDefault().GetValue(null);
            var attributes = (DescriptionAttribute[])typeof(TEnum).GetCustomAttributes(typeof(DescriptionAttribute), false);
            //from attr in attributes where attr.Description==s select 
            return (TEnum)result;
        }
        public static string ToDescriptionString<TEnum>(this TEnum value) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            var attributes = (DescriptionAttribute[])typeof(TEnum).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToDescriptionString<TEnum>(this TEnum? value) where TEnum : struct
        {
            if (value == null) return null;
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            var attributes = (DescriptionAttribute[])typeof(TEnum).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static List<string> ToDescriptionString<TEnum>(this IEnumerable<TEnum> values) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            List<string> result = new List<string>();
            if (values == null) return result;
            foreach (var item in values)
            {
                result.Add(item.ToDescriptionString());
            }
            return result;
        }

        public static List<string> ToDescriptionString<TEnum>(this IEnumerable<TEnum?> values) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            List<string> result = new List<string>();
            if (values == null) return result;
            foreach (var item in values)
            {
                result.Add(item.ToDescriptionString());
            }
            return result;
        }

        public static string IconToClass<TEnum>(this TEnum value) where TEnum : struct
        {
           
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            var attributes = (IconAttribute[])typeof(TEnum).GetField(value.ToString()).GetCustomAttributes(typeof(IconAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : string.Empty;
        }


        public static List<(string, string, TEnum)> GetIconAndDescription<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }
            List<(string, string, TEnum)> result =new List<(string, string, TEnum)>();
            result.AddRange( Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList().Select(e=>( e.ToDescriptionString(), e.IconToClass(),e )));
            return result;
        }
    }
}
