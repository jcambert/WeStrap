using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeTooltipBase : WeTag
    {
        [Inject] protected WeQ.IWeQuery query { get; set; }
        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementReference Tooltip { get; set; }
        protected ElementReference Arrow { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (Target != null)
            {
                var placement = Placement.ToDescriptionString();
                await query.Tooltip(Target, Tooltip, Arrow, "bottom");
            }
        }

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Title { get; set; }
        [Parameter] public string Target { get; set; }

    }
}
