using Microsoft.AspNetCore.Components;
using WeCommon;

namespace WeStrap
{
    public abstract class WeBigPanelBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "") =>
        base.BuildClassName()
                .UsePrefix(CLASS_PREFIX)
                .Add("big_panel")
                .RemovePrefix()
                ;
        [Parameter] public RenderFragment Title { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public RenderFragment Labels { get; set; }
        [Parameter] public string LabelsTitle { get; set; } = string.Empty;
    }
}
