using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public class WeAlertBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("alert")
                .Add(IsDismissible ? "alert-dismissible" : "")
                .Add($"alert-{Color.ToDescriptionString()}");
        }
        [Parameter] public EventCallback<WeEvent<WeAlertBase>> OnClosed { get; set; }
        [Parameter] public EventCallback<WeEvent<WeAlertBase>> OnClose { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsDismissible { get; set; }
        [Parameter]
        public EventCallback OnDismiss { get; set; }
        protected bool IsOpen { get; set; } = true;
        internal WeEvent<WeAlertBase> Event { get; set; }
        internal List<EventCallback<WeEvent<WeAlertBase>>> EventQue { get; set; } = new List<EventCallback<WeEvent<WeAlertBase>>>();
        protected void OnClick(MouseEventArgs args)
        {
            IsOpen = false;
            OnDismiss.InvokeAsync(EventCallback.Empty);
            Event = new WeEvent<WeAlertBase>(this, args);
            OnClose.InvokeAsync(Event);
            EventQue.Add(OnClosed);
        }
        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(Event);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }
    }
}
