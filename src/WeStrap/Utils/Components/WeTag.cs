using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using WeCommon;

namespace WeStrap
{
    public abstract class WeTagBase : WeComponentBase
    {
        internal const string CLASS_PREFIX = "we_{0}";
        protected override WeStringBuilder BuildClassName(string s = "") =>
            base.BuildClassName()
            .Add(s)
            .Add(Spacers.ToDescriptionString())
            .Add("hidden", !IsVisible)
            ;

        protected virtual WeStringBuilder BuildStyle()
        {
            return new WeStringBuilder();
        }
        protected virtual string Tag { get; set; } = "p";


        protected string Style => BuildStyle().ToString();

        protected virtual string disabled => IsDisabled ? "true" : null;
        protected string tab => IsDisabled ? "-1" : null;

        [Parameter] public bool IsVisible { get; set; } = true;

        [Parameter] public bool IsDisabled { get; set; }

        [Parameter]
        public IReadOnlyList<WeSpacer> Spacers { get; set; } = new List<WeSpacer>();
    }
    public abstract class WeTag : WeTagBase
    {

        public WeTag()
        {

        }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public ElementReference Me { get; set; }



    }

    public abstract class WeTagWithEvents : WeTag
    {
        protected virtual void NotifyClick(MouseEventArgs e)
        {
            OnClick.InvokeAsync(new WeMouseEventArgs(e, Me, Id));
        }
        [Parameter] public EventCallback<WeMouseEventArgs> OnClick { get; set; }
        public ElementReference Me { get; set; }
        public string Id { get; set; } = GenerateId();

    }


}
