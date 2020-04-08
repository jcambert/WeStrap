using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeDropdownHeaderBase : WeComponentBase
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s).Add("dropdown-header");
        }
        [Parameter] public RenderFragment ChildContent { get; set; }


    }
}
