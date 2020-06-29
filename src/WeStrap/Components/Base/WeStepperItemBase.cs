using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WeCommon;

namespace WeStrap
{
    public abstract class WeStepperItemBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")=>
            base.BuildClassName(s)
            .Add(Status.ToDescriptionString(),Status!=WeStepperStatus.None)
            ; 
        protected int Index { get; set; }
        [CascadingParameter] internal WeStepperBase Stepper { get; set; }
        [Parameter] public WeStepperStatus Status { get; set; } = WeStepperStatus.None;
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter]public WeStepperItemType StepperType { get; set; }
        [Parameter] public string Label { get; set; } = string.Empty;
        [Parameter] public string IconClass { get; set; } = string.Empty;
        protected override Task OnInitializedAsync()
        {
            if (Stepper == null) throw new ApplicationException("A WeStepperItem must be in WeStepper Tag");
            Stepper.StepperItems.Add(this);
            Index = Stepper.StepperItems.IndexOf(this)+1;
            StepperType = Stepper.StepperType;
            return base.OnInitializedAsync();
        }
    }
}
