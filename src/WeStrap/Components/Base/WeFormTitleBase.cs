using Microsoft.AspNetCore.Components;
using WeCommon;

namespace WeStrap
{
    public abstract class WeFormTitleBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("we_form_title").Add("pb-2");
        }

        [Parameter] public string Title { get; set; } = string.Empty;
    }
}
