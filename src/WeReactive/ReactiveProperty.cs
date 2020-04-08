using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeReactive
{
    [DebuggerDisplay("Value:{LatestValue} - Type:{TypeValue}")]
    public sealed class ReactiveProperty<T>
    {
        #region private variables
        private T _value;
        private readonly Subject<T> ValidationTrigger = new Subject<T>();
        private readonly Subject<T> Source = new Subject<T>();
        private readonly IEqualityComparer<T> equalityComparer;
        #endregion

        #region Public 
        /// <summary>
        /// Set/Get the Value
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                if (equalityComparer?.Equals(_value, value) ?? false)
                    return;
                SetValue(value);
            }
        }

        /// <summary>
        /// Force notification Value Changed,and Validation
        /// </summary>
        public void ForceNotify() => SetValue(_value);

        /// <summary>
        /// Get Value Changed Observable for this property
        /// </summary>
        public IObservable<T> OnValueChanged => Source.AsObservable<T>();

        /// <summary>
        /// Get Valiation Observable for this property
        /// </summary>
        public IObservable<T> OnValidate => ValidationTrigger.AsObservable<T>();

        #endregion
        #region private Methods
        /// <summary>
        /// For Debug
        /// </summary>
        private T LatestValue => _value;

        private string TypeValue => _value.GetType().Name;

        /// <summary>
        /// Set new Value,
        /// Trigger Validation,
        /// Trigger Changed,
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(T value)
        {
            _value = value;
            ValidationTrigger.OnNext(value);
            Source.OnNext(value);

        }

        #endregion
    }
}
