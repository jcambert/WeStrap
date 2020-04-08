using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WePaginationItemBase : WeTag
    {

        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("page-item").Add("active", IsActive).Add("disabled", IsDisabled);



        [Parameter] public bool IsActive { get; set; }
    }
}
