using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WePanelContentBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .UsePrefix(CLASS_PREFIX)
                .Add("content")
                .Add("hide", (Parent?.State == ToggleState.Close));
        }
        [CascadingParameter] public WePanelBase Parent { get; set; }

    }
}
