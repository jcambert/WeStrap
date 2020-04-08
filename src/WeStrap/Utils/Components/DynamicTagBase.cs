using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace WeStrap
{
    public abstract class DynamicTagBase : ComponentBase
    {
        /// <summary>
        /// Render the TagBase 
        /// Must be sequence equals to 0
        /// </summary>
        /// <param name="builder"></param>
       // protected abstract void RenderTag(RenderTreeBuilder builder);


        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> UnMatchedAttributes { get; set; }

        /*protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            base.BuildRenderTree(builder);
            //builder?.OpenElement(0, TagName);
            RenderTag(builder);
            builder.AddMultipleAttributes(1, UnMatchedAttributes);
            
            builder.AddElementReferenceCapture(2, capturedRef =>
            {
                ElementRef = capturedRef;
                ElementRefChanged?.Invoke(ElementRef); // Invoke the callback for the binding to work.
            });
            builder.AddContent(3, ChildContent);

            builder.CloseElement();
        }*/
    }
}
