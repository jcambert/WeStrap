using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeStrap
{
    public abstract class WeStepperItemBase : WeTag
    {

        [CascadingParameter] internal WeStepperBase Stepper { get; set; }
        protected override Task OnInitializedAsync()
        {
            Stepper.StepperItems.Add(this);
            
            return base.OnInitializedAsync();
        }
    }
}
