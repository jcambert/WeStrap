using System.Diagnostics;

namespace WeChart
{
    [DebuggerDisplay("{X} , {Y} , {R}")]
    public struct BubbleData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }
    }
}
