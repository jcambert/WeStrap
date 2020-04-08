using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeToastBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("toast fade")
            .Add("show", IsVisible && !_isDismissed)
            .Add("hide", !IsVisible || _isDismissed);
        }

        [Parameter] public string DateFormat { get; set; } = "dd/MM/yyyy h:mm tt";
        [Parameter] public string ImgSrc { get; set; }
        [Parameter] public string ImgDescription { get; set; }
        [Parameter] public DateTime? TimeStamp { get; set; }
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public RenderFragment WeToastHeader { get; set; }
        [Parameter] public RenderFragment WeToastBody { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        ///</summary>
        [Parameter] public EventCallback OnDismiss { get; set; }

        private bool _isDismissed { get; set; } = false;

        protected async Task OnClick()
        {
            _isDismissed = true;
            await OnDismiss.InvokeAsync(null).ConfigureAwait(false);
            StateHasChanged();
        }
    }
}
