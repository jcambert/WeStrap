using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WeStrap
{
    public class WeValidationMessage<TValue> : WeEditContextBase
    {
        private WeFieldIdentifier _fieldIdentifier;
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
        /// <summary>
        /// Specifies the field for which validation messages should be displayed.
        /// </summary>
        [Parameter] public Expression<Func<TValue>> For { get; set; }

        protected override void OnParametersSet()
        {

            if (For == null) // Not possible except if you manually specify T
            {
                throw new InvalidOperationException($"{GetType()} requires a value for the " +
                    $"{nameof(For)} parameter.");
            }

            _fieldIdentifier = WeFieldIdentifier.Create(For);


            /*   CascadedEditContext?.OnValidationStateChanged().Subscribe(args =>
               {
                   InvokeAsync(() => StateHasChanged());
               });*/
        }
        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            foreach (var message in CascadedEditContext.GetValidationMessages(_fieldIdentifier))
            {
                builder.OpenElement(0, "div");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "class", "validation-message");
                builder.AddContent(3, message);
                builder.CloseElement();
            }
        }
    }



}
