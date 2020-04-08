using Microsoft.AspNetCore.Components;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeListGroupItemHeadingBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("d-flex w-100 justify-content-between");
        }

        [CascadingParameter] public WeListGroup Parent { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Parent == null)
                throw new ArgumentException("WeListGroupItemHeading must be a child of WeListGroup");
        }

    }
}
