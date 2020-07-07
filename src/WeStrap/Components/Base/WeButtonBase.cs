using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WeCommon;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
namespace WeStrap
{



    public abstract class WeButtonBase : WeTag
    {

        public WeButtonBase()
        {
        }
        [CascadingParameter] public IWeEditContext EditContext { get; set; }
        [CascadingParameter] public WeStepperItemBase StepperItem { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsOutline { get; set; } = false;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsBlock { get; set; }
        [Parameter] public string Value { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsLink { get; set; }
        [Parameter] public bool IsLoading { get; set; } = false;
        [Parameter] public string LoadingLabel { get; set; } = "Loading";
        [Parameter] public bool IsCircle { get; set; } = false;


        private ButtonType _buttonType = ButtonType.Button;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (this.ButtonType != ButtonType.StepPrevious)
                StepperItem?.OnStepItemValidAsObservable().Subscribe((WeStepItemValidationArgs args) =>
               {
                   IsDisabled = !args.IsValid;
                   StateHasChanged();
               });
        }
        protected void HandleOnClick(MouseEventArgs evt)
        {
            if (StepperItem != null && (ButtonType == ButtonType.StepNext || ButtonType == ButtonType.StepPrevious))
            {
                HandleStep(StepperItem);
            }
            else
            {
                OnClick.InvokeAsync(evt);
            }
        }
        internal void HandleStep(WeStepperItemBase step)
        {
            if (ButtonType == ButtonType.StepNext)
                step.Next();
            else if (ButtonType == ButtonType.StepPrevious)
                step.Previous();

            //System.Console.WriteLine($"try validate step {step.Index}");
        }
        protected override WeStringBuilder BuildClassName(string s = "") =>
            base.BuildClassName(s).Add("btn")
          .Add($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
          .Add("btn-link", IsLink)
          .Add("btn-circle", IsCircle)
          .Add($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline && !IsLink)
          .Add($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .Add("btn-block", IsBlock)
          .Add("active", ButtonType == ButtonType.Link && IsActive)
          .Add("disabled", _IsDisabled)
          .Add("valid", IsValid)
          .Add("invalid", IsInvalid)
           ;
        private bool _IsDisabled =>
            IsDisabled ||
            ((ButtonType == ButtonType.Button || ButtonType == ButtonType.Submit) && IsLoading) ||
            (ButtonType == ButtonType.Link && (!EditContext?.IsValid() ?? false)) ||
            (ButtonType == ButtonType.Submit && (!EditContext?.IsValid() ?? false));
        protected string type => ButtonType switch
        {
            ButtonType.Input => "button",
            ButtonType.Button => "button",
            ButtonType.Submit => "submit",
            ButtonType.Reset => "reset",
            ButtonType.Checkbox => "checkbox",
            ButtonType.StepNext => "button",
            ButtonType.StepPrevious => "button",
            _ => ""
        };

        [Parameter]
        public ButtonType ButtonType
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                Tag = _buttonType switch
                {
                    ButtonType.Button => "button",
                    ButtonType.Link => "a",
                    ButtonType.Input => "input",
                    ButtonType.Submit => "button",
                    ButtonType.Reset => "input",
                    ButtonType.Radio => "input",
                    ButtonType.Checkbox => "input",
                    _ => "button"
                };
            }
        }




    }
}
