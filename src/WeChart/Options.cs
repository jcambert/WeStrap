using WeC;
namespace WeChart
{
    public interface IOptions
    {
        public bool? Responsive { get; set; }


    }

    public class OptionTitle
    {
        public bool Display { get; set; }
        public string Text { get; set; }
    }
    public class OptionLegend
    {
        public bool? Display { get; set; } = true;
        public string Position { get; set; } = "top";
        public string Align { get; set; } = "center";
        public bool? FullWidth { get; set; } = true;
        public bool? Reverse { get; set; } = false;

    }
    public class OptionTooltip
    {
        public bool Enabled { get; set; } = true;
        public string Custom { get; set; }
        public string Mode { get; set; } = "nearest";
        public bool Intersect { get; set; } = true;
        public string Position { get; set; } = "average";
        public string Callbacks { get; set; }
    }
    public class Options : IOptions
    {
        public bool? Responsive { get; set; } = false;
        public int? ResponsiveAnimationDuration { get; set; } = 0;

        public bool? MaintainAspectRatio { get; set; } = true;

        public double? AspectRatio { get; set; } = 1;   

        public OptionTitle Title { get; set; } = new OptionTitle();

        public OptionLegend Legend { get; set; } = new OptionLegend();

        public WeColor? BackgroundColor{ get; set; }

        public OptionTooltip Tooltips { get; set; } = new OptionTooltip();
    }
}
