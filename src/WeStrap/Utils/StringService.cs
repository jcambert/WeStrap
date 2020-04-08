using System.Threading;
using WeCommon;

namespace WeStrap
{
    public class StringService
    {
        public string ToTitleCase(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value);
        public string ToUpperCase(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(value);
        public string ToLowerCase(string value) =>
            Thread.CurrentThread.CurrentCulture.TextInfo.ToLower(value);
        public string ToPascalCase(string value)
        {
            char separator = ' ';
            WeStringBuilder sb = new WeStringBuilder(value.Split(separator));
            sb.Visit(s => ToUpperCase(s));
            return sb.Join();
        }
        public string ToCamelCase(string value)
        {
            char separator = ' ';
            WeStringBuilder sb = new WeStringBuilder(value.Split(separator));
            sb.Visit(s => ToUpperCase(s));
            sb.Visit(s => ToLowerCase(s), 0);
            return sb.Join();
        }

    }
}
