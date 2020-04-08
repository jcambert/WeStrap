using System.ComponentModel;
namespace WeChart
{

    public enum ChartType
    {
        [Description("bar")]
        Bar,
        [Description("line")]
        Line,
        [Description("radar")]
        Radar,
        [Description("pie")]
        Pie,
        [Description("polarArea")]
        PolarArea,
        [Description("bubble")]
        Bubble,
        [Description("scatter")]
        Scatter,

    }

}