using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using WeCommon;

namespace WeStrap
{
    public class WeStepperBase:WeTag
    {
        private WeStepperItemBase _selected;
        internal List<WeStepperItemBase> StepperItems { get; set; } = new List<WeStepperItemBase>();
        public WeStepperItemBase Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }

        protected override WeStringBuilder BuildClassName(string s = "")=>
            base.BuildClassName(s)
            .UsePrefix(WeTagBase.CLASS_PREFIX)
            .Add("stepper")
            .Add(Type.ToDescriptionString())
            ;
        [Parameter] public WeStepperType Type { get; set; } = WeStepperType.HorizontalNonLinear;
    }
}
