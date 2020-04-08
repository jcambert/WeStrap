using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using WeCommon;
namespace WeStrap
{
    public class WeBreadcrumbBase : WeTag
    {

        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("breadcrumb");
        }
        [Parameter] public IReadOnlyList<WeBreadcrumbValue> Items { get; set; }
        [Parameter] public RenderFragment<WeBreadcrumbValue> Template { get; set; }
        [Parameter] public RenderFragment<WeBreadcrumbValue> TemplateLast { get; set; }
        protected bool IsLastItem(WeBreadcrumbValue item)
        {
            if (!Items.Any()) return false;
            return Items.Last() == item;
        }

        //protected string IsActive(WeBreadcrumbValue item) => IsLastItem(item) ? "active" : string.Empty;

        //protected string AriaCurrent(WeBreadcrumbValue item) => IsLastItem(item) ? "page" : string.Empty;
    }
}
