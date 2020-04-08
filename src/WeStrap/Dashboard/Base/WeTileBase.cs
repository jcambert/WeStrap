using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public abstract class WeTileBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName().UsePrefix(CLASS_PREFIX).Add("tile").Add("stat", IsStatTile).Add("border", HasBorder).Add("clickable",IsClickable).RemovePrefix();
        }
        protected override WeStringBuilder BuildStyle()
        {
            return base.BuildStyle().Add($"background:{BackgroundColor}", !string.IsNullOrEmpty(BackgroundColor));
        }
        [Parameter] public string Title { get; set; }
        [Parameter] public string SubTitle { get; set; }
        [Parameter] public bool HasBorder { get; set; } = true;
        [Parameter] public string BackgroundColor { get; set; } = string.Empty;
        [Parameter] public IReadOnlyList<WeColumn> Columns { get; set; } = new List<WeColumn>() { WeColumn.lg3, WeColumn.md3, WeColumn.sm6 };
        [Parameter] public bool IsStatTile { get; set; } = false;
        [Parameter] public string Icon { get; set; }
        [Parameter] public string Count { get; set; }
        [Parameter] public bool IsClickable { get; set; } = false;
        [Parameter] public EventCallback OnClick { get; set; }
    }

}
