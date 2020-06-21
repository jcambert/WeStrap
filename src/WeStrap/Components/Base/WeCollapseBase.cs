using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using WeCommon;

namespace WeStrap
{
    public abstract class WeCollapseBase : WeToggleableComponent
    {
        [Parameter] public EventCallback<WeCollapseEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<WeCollapseEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<WeCollapseEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<WeCollapseEvent> HiddenEvent { get; set; }
        [Parameter] public bool IsList { get; set; }
        [Parameter] public bool IsNavbar { get; set; }
        [CascadingParameter] protected WeNavBar Navbar { get; set; }
        [CascadingParameter] internal WeCollapseItem CollapseItem { get; set; }
        internal WeCollapseEvent BSCollapseEvent { get; set; }
        internal List<EventCallback<WeCollapseEvent>> EventQue { get; set; } = new List<EventCallback<WeCollapseEvent>>();
        protected string Tag => IsList ? "li" : "div";
        protected bool Collapsing { get; set; }

        protected override WeStringBuilder BuildClassName(string s = "")
        => base.BuildClassName()
            .Add( (Collapsing && IsOpen)  ? "collapsing" : "collapse")
            .Add("navbar-collapse", IsNavbar)
            .Add("show", IsOpen && !Collapsing);
        
    }
}
