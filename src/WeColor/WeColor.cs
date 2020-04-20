using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeCommon;

namespace WeC
{

    public enum WeColorType
    {
        String,
        RGB,
        RGBA,
        HSL,

    }

    public readonly struct WeHSL
    {
        internal const string PATTERN = @"(?<h>\d{1,3}%?)\,(?<s>\d{1,3}%)\,(?<l>\d{1,3}%)\,?(?<a>\d(1|0?\.\d+){1,3})?";
        public static WeHSL White = new WeHSL(0, 0, 1, 1);
        internal WeHSL(Degre h, Percentage s, Percentage l, Percentage a) => (this.Hue, this.Saturation, this.Luminance, this.Alpha) = (h, s, l, a);
        public Degre Hue { get; }
        public Percentage Saturation { get; }
        public Percentage Luminance { get; }
        public Percentage Alpha { get; }

        public override string ToString() => $"hsla({Hue},{Saturation.ToString()},{Luminance.ToString()},{Alpha.m_value})";

        public static WeHSL From(string value) => value.HSL();
        public static implicit operator WeHSL(WeColor value)
        {
            double r = (Percentage)value.R;
            double g = (Percentage)value.G;
            double b = (Percentage)value.B;
            double _min = Math.Min(Math.Min(r, g), b);
            double _max = Math.Max(Math.Max(r, g), b);
            
            double _diff = _max - _min;
            double h = 0;
            double s = 0;
            double l = (_max + _min) / 2.0;
            if (_diff != 0)
            {
                s = l < 0.5 ? _diff / (_max + _min) : _diff / (2.0 - _max - _min);
                h = _max == r ? (g - b) / _diff : _max == g ? 2.0 + (b - r) / _diff : 4.0 + (r - g) / _diff;
            }

            h *= 60;
            h = h < 0 ? h + 360 : h;
            
            return new WeHSL(Math.Round( h,0),s,l,value.Alpha);
        }
    }

    [ComVisible(true)]
    [Serializable]
    public readonly partial struct WeColor
    {
        internal const string PATTERN = @"(?<red>\d{1,3}%?)\,(?<green>\d{1,3}%?)\,(?<blue>\d{1,3}%?)\,?(?<a>\d(1|0?\.\d+){1,3}%?)?";
        public static Percentage OPACITY_FULL = Percentage.MaxValue;
        public static Percentage OPACITY_NONE = Percentage.MinValue;
        internal WeColor(IntH red, IntH green, IntH blue, Percentage alpha)
            => (this.R, this.G, this.B, this.Alpha) =
                (red, green, blue, alpha);

        internal WeColor(IntH red, IntH green, IntH blue)
            => (this.R, this.G, this.B, this.Alpha) =
                (red, green, blue, Percentage.MaxValue);
        internal WeColor((IntH, IntH, IntH, Percentage) rgba)
            => (this.R, this.G, this.B, this.Alpha) =
                (rgba.Item1, rgba.Item2, rgba.Item3, rgba.Item4);
        public IntH R { get; }
        public IntH G { get; }

        public IntH B { get; }

        public Percentage Alpha { get; }


        public override string ToString() => $"#{R.ToStringHex()}{G.ToStringHex()}{B.ToStringHex()}{Alpha.ToStringHex()}";
        public string ToRGB() => $"rgb({R},{G},{B})";
        public string ToRGBA() => $"rgba({R},{G},{B},{Alpha.ToString("0.00",CultureInfo.InvariantCulture)})";



        public static WeColor From(string value) => value.RGB();
        public static WeColor From(IntH red, IntH green, IntH blue, Percentage opacity) => new WeColor(red, green, blue, opacity);

        public static WeColor Random() => WeColor.From(GetRandomColor());

        public static List<WeColor> Random(int nbre)
        {
            var result = new List<WeColor>();
            for (int i = 0; i < nbre; i++)
            {
                result.Add(WeColor.From( GetRandomColor()));
            }
            return result;
        }  
        private static string GetRandomColor()
        {
            Random rnd = new Random();
            var letters = "0123456789ABCDEF";
            WeStringBuilder sb = new WeStringBuilder().Add("#");
            
            for (var i = 0; i < 6; i++)
            {
                sb.Add( letters[rnd.Next(0,15)]);
            }
            return sb.Join();
        }
        public static WeColor operator +(WeColor color, int step) => new WeColor(color.R + step, color.G + step, color.B + step, color.Alpha);
        public static WeColor operator +(WeColor color, (int stepR, int stepG, int stepB) steps) => new WeColor(color.R + steps.stepR, color.G + steps.stepG, color.B + steps.stepB, color.Alpha);
        public static WeColor operator +(WeColor color, (int stepR, int stepG, int stepB, int stepA) steps) => new WeColor(color.R + steps.stepR, color.G + steps.stepG, color.B + steps.stepB, color.Alpha + steps.stepA);

        public static implicit operator WeColor(WeHSL value)
        {
            Func<double, double> adjust = value => value < 0 ? value + 1 : value > 1 ? value - 1 : value;
            Func<double, double, double> calculateRG = (value, defaut) =>
            {
                if ((6 * value) <= 1) return 6 * value;
                if ((2 * value) <= 1) return 2 * value;
                if ((3 * value) <= 1) return 3 * value;
                return defaut;

            };
            Func<double, double, double> calculateB = (value, defaut) =>
            {
                if ((6 * value) <= 1) return 6 * value;
                if ((2.333 * value) <= 1) return 2.333 * value;
                return defaut;
            };
            Func<double, int> Round = value =>
            {
                var result = ((int)(value * 100)) / 100;
                var dec = value - ((int)value);
                return result + (dec > 0.5 ? 1 : 0);
            };

            double tmp1 = Math.Round(value.Luminance < 0.5 ? (double)(value.Luminance * (1.0 + value.Saturation)) : (double)(value.Luminance + value.Saturation - value.Luminance * value.Saturation), 4);
            double tmp2 = Math.Round(2.0 * value.Luminance - tmp1, 4);
            double tmp3 = Math.Round((double)value.Hue / 360, 3);

            double r = tmp3 + 0.333;
            double g = tmp3;
            double b = tmp3 - 0.333;
            r = adjust(r);
            g = adjust(g);
            b = adjust(b);

            var red = (IntH)Round(calculateRG(r, tmp2) * 255);
            var green = (IntH)Round(calculateRG(g, tmp2 + (tmp1 - tmp2) * (0.666 - g) * 6) * 255);
            var blue = (IntH)Round(Math.Round(calculateB(b, tmp1), 2) * 255);

            return WeColor.From(red, green, blue, value.Alpha);
        }
    }

    public static class WeColorExtensions
    {
        internal static WeColor RGB(this string value)
        {
            ColorType colorType;
            if (!value.TryParseColor(out colorType))
                return WeColor.White;
            if (colorType == ColorType.HEX)
            {

                value = value[1..];//remove #
                WeColor color = new WeColor(value.SplitForHexColor());
                return color;

            }
            else if (colorType == ColorType.HSL)
            {
                WeHSL hsl = value.HSL();
                return (WeColor)hsl;
                

            }
            else if (colorType == ColorType.RGB)
            {
                Regex regex = new Regex(WeColor.PATTERN);
                var match = regex.Match(value);

                if (match.Success)
                {
                    var red = (IntH)match.Groups["red"].Value;
                    var green = (IntH)match.Groups["green"].Value;
                    var  blue = (IntH)match.Groups["blue"].Value;
                    Percentage alpha = Percentage.MaxValue;
                    if(match.Groups["a"].Success)
                        alpha = match.Groups["a"].Value;
                    return new WeColor(red, green, blue, alpha);
                }
            }
            else if (colorType == ColorType.STRING)
            {
                return WeColor.Colors[value];
            }
            return WeColor.White;
        }
        internal static WeHSL HSL(this string value)
        {
            ColorType colorType;
            if (!value.TryParseColor(out colorType))
                return WeHSL.White;
            if (colorType == ColorType.HSL)
            {
                Regex regex = new Regex(WeHSL.PATTERN);
                var match = regex.Match(value);
                if (match.Success)
                {
                    var h = (Degre)match.Groups["h"].Value;
                    var s = (Percentage)match.Groups["s"].Value;
                    var l = (Percentage)match.Groups["l"].Value;
                    var a = (Percentage)match.Groups["a"].Value;
                    return new WeHSL(h, s, l, a);
                }
            }
            return WeHSL.White;
        }
        internal static (IntH, IntH) GetMinMax(this WeColor color)
        {
            var c = new List<IntH>() { color.R, color.G, color.B }.OrderBy(c => c);
            return (c.First(), c.Last());
        }

        public static WeColor AdjustAlpha(this WeColor color, Percentage alpha) => WeColor.From(color.R, color.G, color.B,  alpha);
        public static WeColor Opacify(this WeColor color, int alpha) => WeColor.From(color.R, color.G, color.B, color.Alpha - alpha);
        public static WeColor Lightening(this WeColor color, int alpha) => WeColor.From(color.R, color.G, color.B, color.Alpha - alpha);
        public static WeColor Darkening(this WeColor color, int alpha) => WeColor.From(color.R, color.G, color.B, color.Alpha - alpha);
        public static WeColor Filled(this WeColor? from) => WeColor.From(from?.R ?? 0, from?.G ?? 0, from?.B ?? 0, 1);
        internal static string ToStringHex(this IntH value) => value.m_value.ToString("X2");
        internal static string ToStringHex(this Percentage value) => ((IntH)value).ToStringHex();
        internal static int FromHex(this string value) => (int)(IntH)value;


        private static (int, int, int, int) CalculateSteps(WeColor from, WeColor to, int number) =>
             (CalculateStep(from.R, to.R, number), CalculateStep(from.G, to.G, number), CalculateStep(from.B, to.B, number), 0);
        private static int CalculateStep(IntH from, IntH to, int number) => (int)Math.Round((double)(to - from) / number);

        public static List<WeColor> MixWith(this WeColor from, WeColor to, int number = 20)
        {
            List<WeColor> result = new List<WeColor>();

            (int, int, int, int) steps = CalculateSteps(from, to, number);

            WeColor Current = from;
            for (int o = 1; o < number; o++)
            {
                result.Add(Current);
                Current = Current + steps;
            }
            result.Add(to);
            return result;
        }

    }
}
