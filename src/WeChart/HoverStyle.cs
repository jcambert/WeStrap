using System.Collections.Generic;

namespace WeChart
{
    public struct HoverStyle
    {
        public WeC.WeColor? BackgroundColor { get; set; }
        public string BorderCapStyle { get; set; }
        public WeC.WeColor? BorderColor { get; set; }
        public List<int> BorderDash { get; set; }
        public double? BorderDashOffset { get; set; }
        public string BorderJoinStyle { get; set; }
        public double? BorderWidth { get; set; }
    }
}
