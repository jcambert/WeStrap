using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeDropdownMenuBase : WeToggleableComponent
    {


        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("dropdown-menu")
                .Add("show", DropDown?.Selected == this)
                .Add("show", NavItem?.Selected == this)
                .Add("show", NavItem?.Selected != this && DropDown?.Selected != this && IsOpen);
        }



        [Parameter] public bool AutoClose { get; set; }
        [CascadingParameter] internal WeDropdown DropDown { get; set; }
        [CascadingParameter] internal WeNavItem NavItem { get; set; }
        [CascadingParameter] internal WeButtonGroup ButtonGroup { get; set; }

        internal bool IsSubmenu => DropDown == null ? false : DropDown.IsSubmenu;

        //  internal bool Open => DropDown?.Selected == this ? true : NavItem?.Selected == this;

        // public WeModalEvent BSModalEvent { get; private set; }

        public override void Show()
        {
            if (DropDown != null)
            {
                DropDown.Selected = this;
            }
            if (NavItem != null)
            {
                NavItem.Selected = this;
            }
        }

        public override void Hide()
        {
            if (DropDown?.Selected == this)
            {
                DropDown.Selected = null;
            }
            if (NavItem?.Selected == this)
            {
                NavItem.Selected = null;
            }
        }

        public override void Toggle()
        {
            if (DropDown != null)
            {
                DropDown.Selected = DropDown.Selected == this ? null : (this);
            }
            if (NavItem != null)
            {
                NavItem.Selected = NavItem.Selected == this ? null : (this);
            }
        }

        protected override void OnInitialized()
        {
            if (DropDown != null)
            {
                DropDown.DropDownMenu = this;
            }
            if (NavItem != null)
            {
                NavItem.DropDownMenu = this;
            }
            if (AnimationClass == null)
            {
                AnimationClass = "fade";
            }

            base.OnInitialized();
        }

        public void MouseOut()
        {
            if (AutoClose)
            {
                if (DropDown?.Selected == this)
                {
                    DropDown.Selected = null;
                }
                if (NavItem?.Selected == this)
                {
                    NavItem.Selected = null;
                }
            }
        }
    }
}
