using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
namespace WeStrap
{

    public interface IWeValidationSummary : IWeEditContextBase
    {
        List<string> ValidationMessages { get; set; }
        string Title { get; set; }
    }
    public partial class WeValidationSummary : WeEditContextBase, IWeValidationSummary
    {

        /// <summary>
        /// string: Key ,  Tuple of string: Error Message, bool: Valid
        /// </summary>
        public List<string> ValidationMessages { get; set; } = new List<string>();

        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter] public string Title { get; set; } = "";
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            CascadedEditContext.OnValidationStateChanged().Subscribe(args =>
            {
                InvokeAsync(
                    () =>
                    {
                        ValidationMessages.Clear();
                        ValidationMessages.AddRange(CascadedEditContext.GetValidationMessages());
                        StateHasChanged();
                    });

            });
        }

    }
}
