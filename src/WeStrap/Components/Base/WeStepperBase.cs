using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using WeCommon;

namespace WeStrap
{
    public class WeStepperBase:WeTag
    {
        private int _selected;
        internal List<WeStepperItemBase> StepperItems { get; set; } = new List<WeStepperItemBase>();
        public int Selected
        {
            get => _selected;
            set
            {
                if (!Exist(value)) return;
                if (_selected != value)
                    _onStepChanged.OnNext(value);
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }
        private bool Exist(int index) => StepperItems.Where(s => s.Index == index).Any();
        protected override WeStringBuilder BuildClassName(string s = "")=>
            base.BuildClassName(s)
            .UsePrefix(WeTagBase.CLASS_PREFIX)
            .Add("stepper")
            .Add(Type.ToDescriptionString())
            ;
        [Parameter] public WeStepperType Type { get; set; } = WeStepperType.HorizontalNonLinear;
        [Parameter] public WeStepperItemType StepperType { get; set; } = WeStepperItemType.Link;
        [Parameter] public EventCallback<int> OnStepClick { get; set; }
        [Parameter] public EventCallback<int> OnStepCompleted { get; set; }
        [Parameter] public EventCallback<int> OnStepWrong { get; set; }
        [CascadingParameter] public IWeEditContext EditContext { get; set; }
        private ISubject<int> _onStepChanged = new Subject<int>();
        internal IObservable<int> OnStepChangedAsObservable() => _onStepChanged.AsObservable();

        private WeStepperItemBase FindFirstInvalid()
        => StepperItems.OrderBy(x=>x.Index).Where(step=>!step.IsValid).FirstOrDefault() ;

        private WeStepperItemBase FindFirst()
            => StepperItems.OrderBy(x => x.Index).FirstOrDefault();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StepperItems.Clear();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                EditContext?.Validate();
                var step = FindFirstInvalid();
                if (step != null)
                    Go(step.Index);
                else
                    Go(FindFirst()?.Index );
            }
        }
        internal void Previous()
        {
            Go( Selected - 1);
        }

        internal void Next()
        {
           Go( Selected + 1);
        }

        internal void Go(int? index)
        {
            Selected = index ?? 0;
        }
    }
}
