using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeNavbarTogglerBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("navbar-toggler");

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public int MyProperty { get; set; }

        protected async Task Clicked(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e).ConfigureAwait(false);
        }
    }
}
