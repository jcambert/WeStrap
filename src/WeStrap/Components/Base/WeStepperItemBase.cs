using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WeCommon;

namespace WeStrap
{
    public class WeStepItemValidationArgs
    {
        internal WeStepItemValidationArgs(bool isValid, int index) => (IsValid, Index) = (isValid, index);
        public bool IsValid { get;  }
        public int Index { get;  }
    }
    public abstract class WeStepperItemBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")=>
            base.BuildClassName(s)
            .Add(Status.ToDescriptionString(),Status!=WeStepperStatus.None)
            ; 
        public int Index { get; private set; }
        [CascadingParameter] internal WeStepperBase Stepper { get; set; }
        [CascadingParameter] public IWeEditContext EditContext { get; set; }
        [Parameter] public WeStepperStatus Status { get; set; } = WeStepperStatus.None;
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter]public WeStepperItemType StepperType { get; set; }
        [Parameter] public string Label { get; set; } = string.Empty;
        [Parameter] public string IconClass { get; set; } = string.Empty;
        [Parameter] public EventCallback<WeStepItemValidationArgs> OnStepItemValid { get; set; }
        public bool IsValid { get; private set; }

        private System.Reactive.Subjects.ISubject<WeStepItemValidationArgs> _onStepValid = new System.Reactive.Subjects.Subject<WeStepItemValidationArgs>();
        public IObservable<WeStepItemValidationArgs> OnStepItemValidAsObservable() => _onStepValid.AsObservable();
        private List<WeFieldIdentifier> _fields = new List<WeFieldIdentifier>();
        internal void AddFieldIdentifier(WeFieldIdentifier field)
        {
            _fields.Add(field);
        }
        protected override Task OnInitializedAsync()
        {
            if (Stepper == null) throw new ApplicationException("A WeStepperItem must be in WeStepper Tag");
            Stepper.StepperItems.Add(this);
            Index = Stepper.StepperItems.IndexOf(this) + 1;
            StepperType = Stepper.StepperType;
            _fields.Clear();
            EditContext?.OnValidationStateChanged().Subscribe(args =>
            {
                 bool _isValid=true;
                _fields.ForEach(field =>
                {
                    if (EditContext.GetValidationMessages(field).Any())
                    {
                        _isValid = false;
                        return;
                    }
                    _isValid = true;
                });
                WeStepItemValidationArgs _args = new WeStepItemValidationArgs(_isValid, Index);
                _onStepValid.OnNext(_args);
                
            });
            OnStepItemValidAsObservable().Subscribe(args => {
                IsValid = args.IsValid;
                OnStepItemValid.InvokeAsync(args);
            });

            Stepper.OnStepChangedAsObservable().Subscribe(index => {
                UpdateStatus(index);
            });
            //EditContext?.NotifyValidationStateChanged();
            return base.OnInitializedAsync();
        }
        private void UpdateStatus(int index)
        {
            
            if (index == this.Index)
                Status = WeStepperStatus.Active;
            else if (IsValid)
                Status = WeStepperStatus.Completed;
            else if (!IsValid && index < this.Index)
                Status = WeStepperStatus.Wrong;
            else
                Status = WeStepperStatus.None;

            Console.WriteLine($"this index {this.Index} - compare to {index} -> Satus:{Status.ToDescriptionString()}");
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
               
            }
        }
        internal void Previous()
        {
            Stepper.Go(Index-1);
            Console.WriteLine("Want move previous");
        }

        internal void Next()
        {
            Stepper.Go(Index+1);
            Console.WriteLine("Want move next");
        }
    }
}
