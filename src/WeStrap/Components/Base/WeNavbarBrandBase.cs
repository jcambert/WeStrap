using WeCommon;

namespace WeStrap
{
    public abstract class WeNavbarBrandBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "") => base.BuildClassName("navbar-brand");

    }
}