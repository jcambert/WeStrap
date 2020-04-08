using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;
using WeCommon;

namespace WeStrap
{
    public class WeFormFeedback<T> : ValidationMessage<T>, IWeEditContextBase
    {
        private bool _clean = true;
        private WeFieldIdentifier _fieldIdentifier;


        protected string Classname =>
        new WeStringBuilder()


            .Add("valid-tooltip", IsValid && IsTooltip)
            .Add("valid-feedback", IsValid && !IsTooltip)
            .Add("invalid-tooltip", IsInvalid && IsTooltip)
            .Add("invalid-feedback", IsInvalid && !IsTooltip)
            .Add(Class)
            .ToString();

        protected bool HasValidationErrors()
        {
            if (_clean || CascadedEditContext == null)
            {
                _clean = false;
                return false;
            }
            return CascadedEditContext.GetValidationMessages(_fieldIdentifier).Any();
        }

        //[CascadingParameter] protected IWeForm Parent { get; set; }
        [CascadingParameter] public IWeEditContext CascadedEditContext { get; set; }
        public IWeForm Parent => CascadedEditContext?.ParentForm;
        public bool IsValid => !IsInvalid;
        public bool IsInvalid => CascadedEditContext?.GetValidationMessages(_fieldIdentifier).Any() ?? false;
        [Parameter] public bool IsTooltip { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool ShowInvalidMessage { get; set; } = true;
        [Parameter] public bool ShowValidMessage { get; set; } = true;
        /// <summary>
        /// ValidMessage is the string that gets returned if validation is valid.
        /// </summary>
        [Parameter] public string ValidMessage { get; set; }

        /// <summary>
        /// InvalidMessage is the string that gets returned if validation is invalid.
        /// </summary>
        public string InvalidMessage => CascadedEditContext?.GetValidationMessages(_fieldIdentifier).FirstOrDefault() ?? "Veuillez corriger";

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = WeFieldIdentifier.Create(For);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            if (IsValid == true && ShowValidMessage)
            {
                builder?.OpenElement(0, "div");
                builder.AddAttribute(1, "class", Classname);
                builder.AddContent(6, ValidMessage);
                builder.CloseElement();
            }
            else if (IsInvalid && ShowInvalidMessage)
            {

                builder?.OpenElement(0, "div");
                builder.AddAttribute(1, "class", Classname);
                builder.AddContent(6, InvalidMessage);
                builder.CloseElement();

            }
            /* else

             {
                 if (ShowInvalidMessage)
                 {
                     builder?.OpenElement(0, "div");
                     builder.AddAttribute(1, "class", Classname);
                     builder.OpenComponent<WeValidationMessage<T>>(2);
                     builder.AddMultipleAttributes(3, AdditionalAttributes);
                     builder.AddAttribute(4, "For", For);
                     builder.CloseComponent();
                     builder.AddContent(5, ChildContent);
                     builder.CloseElement();
                 }
             }*/
        }
    }
}
