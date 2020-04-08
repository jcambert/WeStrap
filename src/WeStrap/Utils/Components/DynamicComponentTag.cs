using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;


namespace WeStrap
{
    public class DynamicComponentTag<TComponent> : DynamicTagBase
        where TComponent : IComponent
    {
        private RenderFragment _renderFragment;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {



            builder.OpenComponent<TComponent>(0);

            builder.AddMultipleAttributes(1, UnMatchedAttributes);

            builder.AddContent(2, ChildContent);

            builder.CloseComponent();


        }
    }
}
