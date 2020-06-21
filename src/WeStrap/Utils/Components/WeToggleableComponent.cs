using Microsoft.AspNetCore.Components;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeStrap
{
    public class WeToggleState
    {
        public bool Manual { get; set; }

        public ToggleState State { get; set; }
    }
    public abstract class WeToggleableComponent : WeComponentBase
    {
        public WeToggleableComponent()
        {

        }
        private Subject<ToggleState> _onChanged = new Subject<ToggleState>();
        internal ElementReference MyRef { get; set; }
        [Parameter] public EventCallback<ToggleState> IsOpenChanged { get; set; }
        [Parameter] public string AnimationClass { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter]
        public bool Open
        {
            get => State == ToggleState.Open;
            set
            {
                bool fireChanged = false;
                fireChanged = value != IsOpen;
                State = value ? ToggleState.Open : ToggleState.Close;
                FireChanged(fireChanged);


            }
        }

        public IObservable<ToggleState> OnChanged => _onChanged.AsObservable();

        private ToggleState _state = ToggleState.Close;
        public virtual ToggleState State { get=>_state; protected set=>_state=value; } 

        public bool IsOpen => State == ToggleState.Open;

        public bool IsClose => State == ToggleState.Close;

        protected void FireChanged(bool hasChanged = true)
        {
            if (!hasChanged) return;
            _onChanged.OnNext(State);
            IsOpenChanged.InvokeAsync(State);
        }

        public virtual void Show()
        {
            Open = true;
        }
        public virtual void Hide()
        {
            Open = false;
        }
        public virtual void Toggle()
        {
            Open = !Open;

        }
    }
}
