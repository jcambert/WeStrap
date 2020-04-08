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
    public class Options : IOptions
    {
        public bool? Responsive { get; set; } = false;
        public int? ResponsiveAnimationDuration { get; set; } = 0;

        public bool? MaintainAspectRatio { get; set; } = true;

        public int? AspectRatio { get; set; } = 1;



        public OptionTitle Title { get; set; } = new OptionTitle();

        public OptionLegend Legend { get; set; } = new OptionLegend();
    }
}
