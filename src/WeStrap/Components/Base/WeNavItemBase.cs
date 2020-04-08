using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeNavItemBase : WeToggleableComponent, IDisposable
    {
        internal bool IsSubmenu { get; set; }


        private WeDropdownMenuBase _selected;
        private bool _shouldClose { get; set; } = false;

        //Prevents NULL
        private WeDropdownMenuBase _dropDownMenu { get; set; } = new WeDropdownMenu();

        public WeDropdownMenuBase DropDownMenu
        {
            get => _dropDownMenu;
            set
            {
                _dropDownMenu = value;
                StateHasChanged();
            }
        }

        public WeDropdownMenuBase Selected
        {
            get
            {
                if (Nav != null)
                {
                    if (Nav.Selected != this)
                    {
                        return null;
                    }
                }
                return _selected;
            }
            set
            {
                if (value == null)
                {
                    _selected = null;
                    Nav.Selected = null;
                }
                else
                {
                    Nav.Selected = this;
                    _selected = value;
                }
            }
        }

        private bool _active;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                StateHasChanged();
            }
        }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("nav-item", !RemoveDefaultClass)
                .Add("dropdown", IsDropdown)
                .Add("dropdown-submenu", IsSubmenu)
                .Add("show", IsDropdown && (Nav?.Selected == this || IsOpen))
                .Add("active", _active);
        }


        protected string Tag => Nav.IsList ? "li" : "div";
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public bool CloseOnFocusout { get; set; } = true;

        [CascadingParameter] internal WeNavBase Nav { get; set; }
        [CascadingParameter] internal WeNavItem NavItem { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (IsDropdown /*&& !Manual*/)
            {
                Nav.Navitems.Add(this);
            }
            if (NavItem != null)
            {
                IsSubmenu = true;
            }
            return base.OnInitializedAsync();
        }

        protected void MouseLeave()
        {
            _shouldClose = true;
        }

        protected void MouseEnter()
        {
            _shouldClose = false;
        }

        public override void Toggle()
        {
            if (Nav.Selected == this)
            {
                Selected = null;
            }
            else
            {
                Selected = DropDownMenu;
            }
            base.Toggle();
        }

        public override void Show()
        {
            Selected = DropDownMenu;
            base.Show();
        }

        public override void Hide()
        {
            Selected = null;
            base.Hide();
        }

        protected void LostFocus()
        {
            if (!CloseOnFocusout)
            {
                return;
            }
            if (_shouldClose)
            {
                if (Nav.Selected == this)
                {
                    Selected = null;
                }
                else
                {
                    /* if (Manual)
                     {*/
                    Hide();
                    /* }*/
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDropdown && disposing)
            {
                Nav.Navitems.Remove(this);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}
