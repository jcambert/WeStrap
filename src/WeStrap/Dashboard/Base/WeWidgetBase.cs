using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public abstract class WeWidgetBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName().Add(Columns.ToDescriptionString()).UsePrefix(CLASS_PREFIX).Add("widget").RemovePrefix();
        }
        [Parameter] public IReadOnlyList<WeColumn> Columns { get; set; }
    }
}
