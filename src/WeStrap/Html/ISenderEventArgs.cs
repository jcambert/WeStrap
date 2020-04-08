using Microsoft.AspNetCore.Components;

namespace WeStrap
{
    public interface ISenderEventArgs
    {
        ElementReference Sender { get; }
    }
}
