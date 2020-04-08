using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeNavLinkBase : WeTag
    {
        [Inject] protected NavigationManager UriHelper { get; set; }
        [CascadingParameter] protected WeNavItem Parent { get; set; }
        // [CascadingParameter] protected WeCollapseItem CollapseItem { get; set; }
        //[CascadingParameter] protected WeListItem ListItem { get; set; }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("nav-item nav-link", !RemoveDefaultClass)
            .Add("active", IsActive)
            .Add("disabled", IsDisabled);
        }


        protected string Disabled => IsDisabled ? "disabled" : null;

        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public bool IsActive { get; set; }

        [Parameter] public string Href { get; set; }

        protected override void OnInitialized()
        {
            UriHelper.LocationChanged += OnLocationChanged;
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.Uri, true));
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UriHelper.LocationChanged -= OnLocationChanged;
            }
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var active = e?.Location.MatchActiveRoute(UriHelper.BaseUri + Href) ?? false;

            if (Parent != null)
            {
                Parent.Active = active;
            }
            /*if (CollapseItem != null)
            {
                CollapseItem.Active = active;
            }
            if (ListItem != null)
            {
                ListItem.Active = active;
            }*/
            IsActive = active;
            StateHasChanged();
        }
    }
}
