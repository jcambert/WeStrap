using Microsoft.AspNetCore.Components;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WePanelBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName()
                .UsePrefix(CLASS_PREFIX)
                .Add("panel")
                .Add("ribbon-container", Ribbon != null)
                .Add(FixedHeight.ToDescriptionString())
                .RemovePrefix()
                ;
        }
        [Parameter] public FixedHeight FixedHeight { get; set; } = FixedHeight.None;
        [Parameter] public RenderFragment Ribbon { get; set; }

        [Parameter] public RenderFragment Title { get; set; }

        [Parameter] public RenderFragment Content { get; set; }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

        }

        ToggleState _state;
        public ToggleState State
        {
            get => _state;
            private set
            {
                if (value != _state)
                {
                    _state = value;

                    StateHasChanged();
                }
            }
        }
        internal void SetTitle(WePanelTitleBase title)
        {
            title.OnChanged.Subscribe(state =>
            {
                this.State = state;
            });
        }

        /*   WePanelContentBase _content;
           internal void SetContent(WePanelContentBase content)
           {
               this._content = content;
           }*/
    }
}
