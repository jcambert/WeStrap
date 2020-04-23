using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WeCommon;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace WeStrap
{
    public abstract class WePopoverBase : WeToggleableComponent
    {
        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("popover").Add($"bs-popover-{Placement.ToDescriptionString()}");
        [Parameter] public bool IsHoverable { get; set; } = true;
        [Inject] public WeQ.IWeQuery query { get; set; }
        protected ElementReference Arrow { get; set; }
        protected ElementReference ThePopper { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun && DismissOnNext)
            {
                Container?.RegisterPopover(this);

                var placement = Placement.ToDescriptionString();
                ThePopper = await query.Popper(Target, Id, Arrow, placement,IsHoverable);
            }

            this.OnChanged.Subscribe(state =>
            {
                if (state == ToggleState.Open)
                    query.PopperShow(Id);
                else if (state == ToggleState.Close)
                    query.PopperHide(Id);

            });

            // if (Open)
            //{

            //}
        }


        protected string Id => Target + "-popover";
        [Parameter] public bool DismissOnNext { get; set; } = true;
        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Target { get; set; }
        [Parameter] public string Style { get; set; }
        [CascadingParameter] public WeContainer Container { get; set; }
        protected void OnClick()
        {
            Hide();
        }
    }
}
