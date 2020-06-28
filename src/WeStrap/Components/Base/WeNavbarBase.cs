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
            .Add("reactive",IsReactive)
            .Add("bottom",IsReactive && (ReactivePosition== NavbarReactivePosition.BottomLeft || ReactivePosition == NavbarReactivePosition.BottomRight ))
            .Add("right", IsReactive && (ReactivePosition == NavbarReactivePosition.BottomRight || ReactivePosition == NavbarReactivePosition.TopRight))
            .Add("fixed-top", IsFixedTop && !IsReactive)
            .Add("fixed-bottom", IsFixedBottom && !IsReactive)
            .Add("sticky-top", IsStickyTop && !IsReactive)
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
        [Parameter] public bool IsReactive { get; set; }
        [Parameter] public NavbarReactivePosition ReactivePosition { get; set; } = NavbarReactivePosition.BottomLeft;
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
