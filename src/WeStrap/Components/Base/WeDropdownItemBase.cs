using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeDropdownItemBase : WeComponentBase, IDisposable
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Inject] protected NavigationManager UriHelper { get; set; }

        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("dropdown-divider", IsDivider)
                .Add("dropdown-item", !IsDivider)
                .Add("active", !IsDivider && IsActive)
                .Add("disabled", IsDisabled);
        }



        protected string Tag => IsDivider ? "div" : IsButton ? "button" : "a";

        protected string Type => IsButton ? "button" : null;

        internal bool HasSubMenu { get; set; }
        [Parameter] public bool IsDivider { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool StayOpen { get; set; }
        [Parameter] public string Href { get; set; } = "javascript:void(0)";
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] internal WeDropdown DropDown { get; set; }

        protected void OnClickEvent(MouseEventArgs e)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(e);
            }
            if (!StayOpen && DropDown?.IsSubmenu == false && !HasSubMenu)
            {
                DropDown.Selected = null;
            }
        }

        protected override void OnInitialized()
        {
            UriHelper.LocationChanged += OnLocationChanged;
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.Uri, true));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UriHelper.LocationChanged -= OnLocationChanged;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var active = e?.Location.MatchActiveRoute(UriHelper.BaseUri + Href) ?? false;
            if (active != IsActive)
            {
                if (DropDown != null)
                {
                    DropDown.Active = active;
                }
                IsActive = active;
            }
        }
    }
}
