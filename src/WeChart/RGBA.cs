using System.Globalization;

namespace WeChart
{

    public readonly struct RGBA
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
        public double Opacity { get; }
        public RGBA(int red, int green, int blue, double opacity) => (this.Red, this.Green, this.Blue, this.Opacity) = (red, green, blue, opacity);

        public override string ToString() => $"rgba({Red},{Green},{Blue},{Opacity.ToString("0.00", CultureInfo.GetCultureInfo("en-US"))})";

        public static RGBA Filled(RGBA? from) => new RGBA(from?.Red ?? 0, from?.Green ?? 0, from?.Blue ?? 0, 1);
    }
}
