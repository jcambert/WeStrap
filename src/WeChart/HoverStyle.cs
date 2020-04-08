using System.Collections.Generic;

namespace WeChart
{
    public struct HoverStyle
    {
        public RGBA? BackgroundColor { get; set; }
        public string BorderCapStyle { get; set; }
        public RGBA? BorderColor { get; set; }
        public List<int> BorderDash { get; set; }
        public double? BorderDashOffset { get; set; }
        public string BorderJoinStyle { get; set; }
        public double? BorderWidth { get; set; }
    }
}
