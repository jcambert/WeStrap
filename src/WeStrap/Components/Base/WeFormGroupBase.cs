using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeFormGroupBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("form-check", IsCheck)
           .Add("form-group", !IsCheck)
           .Add("row", IsRow);
        }

        [Parameter] public bool IsRow { get; set; }
        [Parameter] public bool IsCheck { get; set; }
    }
}
