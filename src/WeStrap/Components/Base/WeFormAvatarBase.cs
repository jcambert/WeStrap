using Microsoft.AspNetCore.Components;
using WeCommon;

namespace WeStrap
{
    public abstract class WeFormAvatarBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("we_form_avatar").Add("pb-2");
        }
        [Parameter] public string Image { get; set; }
    }
}
