using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WePaginationBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("pagination").Add($"pagination-{Size.ToDescriptionString()}", Size != Size.None).Add(GetAlignment());



        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public Alignment Alignment { get; set; } = Alignment.Left;
        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }
    }
}
