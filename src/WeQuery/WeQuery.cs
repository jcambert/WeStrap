using Microsoft.AspNetCore.Components;

namespace WeQ
{

    public sealed partial class WeQuery : ComponentBase
    {

        public WeQuery()
        {

        }
        [Inject]
        public IWeQuery Query { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }


    }
}
