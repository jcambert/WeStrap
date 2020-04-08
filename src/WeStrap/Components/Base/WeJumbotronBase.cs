using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeJumbotronBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("jumbotron jumbotron-fluid", IsFluid)
            .Add("jumbotron", !IsFluid);
        }


        [Parameter] public bool IsFluid { get; set; }

    }
}
