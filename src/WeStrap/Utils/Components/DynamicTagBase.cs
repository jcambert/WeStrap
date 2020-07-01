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

    }
}
