using System.Collections.Generic;
using WeC;
namespace WeChart
{
    public struct BorderStyle
    {
        public int Width { get; set; }
        public WeC.WeColor? Color { get; set; }



        public List<int> Dash { get; set; }
        public int? DashOffset { get; set; }
        public LineJoin? JoinStyle { get; set; }
    }
}
