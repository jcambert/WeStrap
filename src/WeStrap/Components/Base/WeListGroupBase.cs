using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeListGroupBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("list-group list-group-flush", IsFlush)
            .Add("list-group", !IsFlush);
        }

        protected override string Tag => ListGroupType == ListGroupType.List ? "ul" : "div";

        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public bool IsFlush { get; set; }

    }
}
