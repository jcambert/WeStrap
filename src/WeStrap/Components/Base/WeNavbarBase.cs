using Microsoft.AspNetCore.Components;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeNavbarBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("navbar")
            .Add("fixed-top", IsFixedTop)
            .Add("fixed-bottom", IsFixedBottom)
            .Add("sticky-top", IsStickyTop)
            .Add("navbar-dark", IsDark)
            .Add("bg-dark", IsDark)
            .Add("navbar-light", !IsDark && !HideLight)
            .Add($"bg-{BackgroundColor.ToDescriptionString()}", BackgroundColor != Color.None)
            .Add(Expand.ToDescriptionString(), Expand != NavBarExpand.None)
            ;
        }
        [Parameter] public Color BackgroundColor { get; set; } = Color.None;
        [Parameter] public bool IsDark { get; set; }
       // [Parameter] public bool IsExpand { get; set; }
        [Parameter] public bool IsFixedBottom { get; set; }
        [Parameter] public bool IsFixedTop { get; set; }
        [Parameter] public bool IsStickyTop { get; set; }
        [Parameter] public bool HideLight { get; set; } = false;
        [Parameter] public NavBarExpand Expand { get; set; } = NavBarExpand.None;
        internal bool HasCollapsed { get; set; }
        internal bool Visible
        {
            get => _visible;
            set
            {
                VisibleChange.Invoke(this, value);
                _visible = value;
            }
        }


        internal EventHandler<bool> VisibleChange { get; set; }


        private bool _visible { get; set; }
    }

}
