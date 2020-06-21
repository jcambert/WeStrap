using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public abstract class WeNavBase : WeComponentBase, IDisposable
    {

        private WeNavItemBase _selected;
        internal List<WeNavItemBase> Navitems { get; set; } = new List<WeNavItemBase>();
        public WeNavItemBase Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("nav", !RemoveDefaultClass)
            .Add("navbar-nav", IsNavbar)
            .Add("nav-tabs", IsTabs)
            .Add("nav-pills", IsPills)
            .Add("nav-fill", IsFill)
            .Add("flex-column", IsVertical)
            .Add(GetAlignment())
            .Add(WeSpacer.MarginRightAuto.ToDescriptionString(),MarginRightAuto)
            .Add(WeSpacer.MarginLeftAuto.ToDescriptionString(), MarginLeftAuto)
            ;
        }


        protected string Tag => IsList ? "ul" : "nav";

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool IsList { get; set; }
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTabs { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public bool MarginLeftAuto { get; set; }
        [Parameter] public bool MarginRightAuto { get; set; }

        private string GetAlignment()
        {
            return Alignment == Alignment.Center ? "justify-content-center" : Alignment == Alignment.Right ? "justify-content-end" : null;
        }

        protected override void OnInitialized()
        {
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
                _selected?.Dispose();
            }
        }
    }
}
