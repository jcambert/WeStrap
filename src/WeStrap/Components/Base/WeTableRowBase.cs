using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeTableRowBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add($"table-{Color.ToDescriptionString()}", Color != Color.None);
        }


        [Parameter] public Color Color { get; set; } = Color.None;
    }
}
