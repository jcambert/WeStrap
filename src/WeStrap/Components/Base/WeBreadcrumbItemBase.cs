using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public class WeBreadcrumbItemBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("breadcrumb-item")
                .Add("active", IsActive);
        }
        protected string aria => IsActive ? "page" : null;

        [Parameter] public bool IsActive { get; set; }
    }
}
