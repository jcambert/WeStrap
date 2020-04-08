using System.Collections.Generic;

namespace WeChart
{
    public struct BorderStyle
    {
        public int Width { get; set; }
        public RGBA? Color { get; set; }



        public List<int> Dash { get; set; }
        public int? DashOffset { get; set; }
        public LineJoin? JoinStyle { get; set; }
    }
}
