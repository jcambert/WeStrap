using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WeStrap
{
    public class WeMouseEventArgs : ISenderEventArgs
    {
        public WeMouseEventArgs(MouseEventArgs e, ElementReference sender, string id)
        {
            MouseEventArgs = e;
            Sender = sender;
            Id = id;
        }
        public ElementReference Sender { get; }
        public string Id { get; }
        public MouseEventArgs MouseEventArgs { get; }
    }
}
