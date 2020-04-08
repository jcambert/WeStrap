using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WePopoverBase : WeToggleableComponent
    {
        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("popover").Add($"bs-popover-{Placement.ToDescriptionString()}").Add("show", IsOpen);
        //[Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        [Inject] public WeQ.IWeQuery query { get; set; }
        protected ElementReference Arrow { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun && DismissOnNext)
            {
                Container?.RegisterPopover(this);
            }
            if (Open)
            {

                var placement = Placement.ToDescriptionString();
                await query.Popper(Target, Id, Arrow, placement);

            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            /*  this.OnChanged.Subscribe( state => {
                  Console.WriteLine($"State:{state.State.ToDescriptionString()}");
              });*/
            /*if(DismissOnNext)
                Container?.RegisterPopover(this);*/
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
