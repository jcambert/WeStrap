using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeRowBase : WeTag
    {
        public WeRowBase()
        {

        }


        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)

            .Add("row no-gutters", NoGutters)
            .Add("row", !NoGutters)
            .Add(RowColumnCount.ToDescriptionString())
            .Add(RowColumnSmallCount.ToDescriptionString())
            .Add(RowColumnMediumCount.ToDescriptionString())
            .Add("w-100", IsBreak)
            .Add(() =>
            {
                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        return "align-items-end";
                    case VerticalAlignment.Center:
                        return "align-items-center"; ;
                    case VerticalAlignment.Top:
                        return "align-items-start"; ;
                    default:
                        break;
                }
                return string.Empty;
            }, true)
            .Add(() =>
            {
                switch (HorizontalAlignment)
                {
                    case Alignment.None:
                        break;
                    case Alignment.Left:
                        return "justify-content-start";
                    case Alignment.Center:
                        return "justify-content-center";
                    case Alignment.Right:
                        return "justify-content-end";
                    default:
                        break;
                }
                return string.Empty;
            }, true)
            ;
        }

        protected override WeStringBuilder BuildStyle()
        {
            return base.BuildStyle()
                .Add($"background-color:{BackgroundColor}", BackgroundColor.Trim().Length > 0)
                .Add($"display:{ Display?.ToDescriptionString()}", Display != null)
                ;
        }
        [Parameter] public bool NoGutters { get; set; }
        [Parameter] public bool IsBreak { get; set; } //.w-100


        [Parameter] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;

        [Parameter] public Alignment HorizontalAlignment { get; set; } = Alignment.None;


        [Parameter]
        public WeRowCount RowColumnCount { get; set; } = WeRowCount.None;


        [Parameter]
        public WeRowSmallCount RowColumnSmallCount { get; set; } = WeRowSmallCount.None;

        [Parameter]
        public WeRowMediumCount RowColumnMediumCount { get; set; } = WeRowMediumCount.None;

        [Parameter]
        public CssDisplay? Display { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; } = string.Empty;



    }
}
