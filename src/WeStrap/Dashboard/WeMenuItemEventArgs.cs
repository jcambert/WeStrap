using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WeStrap
{
    public class WeMenuItemEventArgs<TItem> : WeMouseEventArgs
        where TItem : WeMenuItem
    {
        public WeMenuItemEventArgs(MouseEventArgs e, ElementReference sender, string id, TItem item) : base(e, sender, id)
        {
            this.Item = item;
        }
        public WeMenuItemEventArgs(WeMouseEventArgs args, TItem item) : base(args.MouseEventArgs, args.Sender, args.Id)
        {
            this.Item = item;
        }

        public TItem Item { get; }
    }
}
