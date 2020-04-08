using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeDropdownToggleBase : WeComponentBase
    {

        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
             .Add("btn", !IsLink)
             .Add("dropdown-item", Dropdown?.IsSubmenu == true)
             .Add($"btn-{Size.ToDescriptionString()}", !IsLink && Size != Size.None)
             .Add($"btn-{Color.ToDescriptionString()}", !IsLink && Color != Color.None)
             .Add("dropdown-toggle-split", IsSplit)
             .Add("dropdown-toggle")
             //nav-link should only show on root drop down toggle
             .Add("nav-link", IsLink && Dropdown?.NavItem != null && Dropdown?.IsSubmenu == false);
        }


        protected string Tag => IsLink ? "a" : "button";
        protected string Type => IsLink ? null : "button";
        protected string Href => IsLink ? "javascript:void(0)" : null;

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsLink { get; set; } = false;

        [Obsolete("This Parameter is no longer require and will be removed soon")]
        [Parameter] public bool? IsOpen { get; set; }

        [Parameter] public bool IsSplit { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] internal WeDropdownMenuBase Dropdown { get; set; }

        protected void Escape(KeyboardEventArgs e)
        {
            if (Dropdown == null)
                return;
            if (e?.Key.ToUpperInvariant() == "ESCAPE")
            {
                Dropdown.Hide();
            }
        }

        protected void OnClickEvent(MouseEventArgs e)
        {
            OnClick.InvokeAsync(e);
            if (Dropdown == null)
                return;

            Dropdown.Toggle();

        }
    }
}
