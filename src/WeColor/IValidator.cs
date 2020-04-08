using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WeC
{
    
    internal enum ColorType{
        NONE,
        STRING,
        HEX,
        RGB,
        HSL,
    }
    internal interface IValidator
    {
        bool IsValid(string value);
    }

    internal abstract class Validator : IValidator
    {
        protected abstract string Pattern { get; }
        public virtual bool IsValid(string value)=>new Regex(Pattern).IsMatch(value);
    }

    internal class ColorHexValidator : Validator
    {
        protected override string Pattern => "^#([A-Fa-f0-9]{8}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{4}|[A-Fa-f0-9]{3})$";
    }


    internal class ColorRgbaHslaValidator : Validator
    {
        protected override string Pattern => @"((rgb|hsl)a\((\d{1,3}%?,\s?){3}(1|0?\.\d+)\)|(rgb|hsl)\(\d{1,3}%?(,\s?\d{1,3}%?){2}\))";
    }

    internal class ColorNameValidator : IValidator
    {
        public bool IsValid(string value)=>WeColor.Colors.ContainsKey(value);
    }
    internal static class ColorExtensions
    {
        internal static bool TryParseColor(this string color,out ColorType type)
        {
            bool result;
            
            (result, type) =   (TryParseColorHex(color), ColorType.HEX) ;
            if (result) return result;

            (result, type) = (TryParseColorRgbaHsla(color), color.Contains("rgb")? ColorType.RGB:ColorType.HSL);
            if (result) return result;

            (result,type)= (TryParseColorName(color), ColorType.STRING);
            if (result) return result;

            if (!result) type = ColorType.NONE;
            return result;
        }

        private static bool TryParseColorHex(string color) => new ColorHexValidator().IsValid(color);

        private static bool TryParseColorRgbaHsla(string color) => new ColorRgbaHslaValidator().IsValid(color);

        private static bool TryParseColorName(string color) => new ColorNameValidator().IsValid(color);

        internal static (IntH, IntH, IntH, Percentage) SplitForHexColor(this string value)
        {
            string []values = new string[4];
            int step = value.Length>4? 2:1;
            int idx = 0;
            string tmp;
            for (int i = idx; i < value.Length; i+=step)
            {
                tmp = value.Substring(i , step);

                values[idx++] = tmp.Length==1?tmp+tmp:tmp;
            }
            if (values[3] == null) 
                values[3] = Percentage.MaxValue.ToString();
            return ((IntH) values[0], (IntH)values[1], (IntH)values[2],(Percentage) values[3] );
        }
    }
}
