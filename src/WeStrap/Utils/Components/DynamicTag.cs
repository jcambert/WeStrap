using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace WeStrap
{
    public class DynamicTag : DynamicTagBase
    {
        // <summary>
        /// Gets or sets the name of the element to render.
        /// </summary>
        [Parameter] public string TagName { get; set; }
        [Parameter] public ElementReference ElementRef { get; set; }
        [Parameter] public Action<ElementReference> ElementRefChanged { get; set; }
        //protected override void RenderTag(RenderTreeBuilder builder) => builder?.OpenElement(0, TagName);

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            base.BuildRenderTree(builder);
            builder?.OpenElement(0, TagName);
            // RenderTag(builder);
            builder.AddMultipleAttributes(1, UnMatchedAttributes);
            builder.AddContent(3, ChildContent);
            builder.AddElementReferenceCapture(2, capturedRef =>
            {
                ElementRef = capturedRef;
                ElementRefChanged?.Invoke(ElementRef); // Invoke the callback for the binding to work.
            });


            builder.CloseElement();
        }
    }
}
