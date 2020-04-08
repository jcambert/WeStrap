using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public class WeButtonGroupBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("btn-toolbar", IsToolbar)
            .Add("btn-group", !IsToolbar && !IsVertical)
            .Add("btn-group-vertical", !IsToolbar && IsVertical)
            .Add($"btn-group-{Size.ToDescriptionString()}", Size != Size.None)
            .Add("btn-group-toggle", IsToggle)
            .Add(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .Add("show", IsOpen);
        }

        [Parameter] public bool IsOpen { get; set; }
        [Parameter] public bool IsToggle { get; set; }
        [Parameter] public bool IsToolbar { get; set; }
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] public Size Size { get; set; } = Size.None;
    }
}
