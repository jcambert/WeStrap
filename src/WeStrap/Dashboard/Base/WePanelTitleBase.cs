using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WePanelTitleBase : WeToggleableComponent
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .UsePrefix(WeTag.CLASS_PREFIX)
                .Add("title");
        }
        [CascadingParameter] public WePanelBase Parent { get; set; }
        [Parameter] public string Title { get; set; } = "";
        [Parameter] public string SubTitle { get; set; } = "";
        [Parameter] public bool IsCollapseable { get; set; }
        [Parameter] public bool IsCloseable { get; set; }
        [Parameter] public RenderFragment ToolBox { get; set; }
        [Parameter] public RenderFragment TitleContent { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        public override ToggleState State { get; protected set; } = ToggleState.Open;

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender && Parent != null)
            {
                Parent.SetTitle(this);
            }
        }
    }
}
