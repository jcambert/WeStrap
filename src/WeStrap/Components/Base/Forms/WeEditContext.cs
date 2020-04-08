using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WeStrap
{

    public interface IWeEditContext : IDisposable
    {

        IObservable<WeFieldChangedEventArgs> OnFieldChanged();
        IObservable<WeValidationRequestedEventArgs> OnValidationRequested();
        IObservable<WeValidationStateChangedEventArgs> OnValidationStateChanged();
        IObservable<WeValidationStateChangedEventArgs> OnFormValid();
        void NotifyFieldChanged(in WeFieldIdentifier fieldIdentifier);
        void NotifyValidationStateChanged();
        void NotifyValidationStateChanged(in WeFieldIdentifier fieldIdentifier);
        WeFieldIdentifier Field(string fieldName);
        void MarkAsUnmodified(in WeFieldIdentifier fieldIdentifier);
        void MarkAsUnmodified();
        bool IsModified();
        bool IsModified(in WeFieldIdentifier fieldIdentifier);
        bool IsValid();

        bool IsValid(WeFieldIdentifier fieldIdentifier);

        IEnumerable<string> GetValidationMessages();
        IEnumerable<string> GetValidationMessages(WeFieldIdentifier fieldIdentifier);
        IEnumerable<string> GetValidationMessages(Expression<Func<object>> accessor);
        bool Validate();
        bool Validate(WeFieldIdentifier fieldIdentifier);
        IWeForm ParentForm { get; }

        object GetModel();

        Type GetModelType();

        WeFieldState GetFieldState(in WeFieldIdentifier fieldIdentifier, bool ensureExists);
    }
    public sealed class WeEditContext<TModel> : IWeEditContext
        where TModel : class, new()
    {
        // Note that EditContext tracks state for any FieldIdentifier you give to it, plus
        // the underlying storage is sparse. As such, none of the APIs have a "field not found"
        // error state. If you give us an unrecognized FieldIdentifier, that just means we
        // didn't yet track any state for it, so we behave as if it's in the default state
        // (valid and unmodified).
        private readonly Dictionary<WeFieldIdentifier, WeFieldState> _fieldStates = new Dictionary<WeFieldIdentifier, WeFieldState>();
        private readonly ISubject<WeFieldChangedEventArgs> _onFieldChanged = new Subject<WeFieldChangedEventArgs>();
        private readonly ISubject<WeValidationRequestedEventArgs> _onValidationRequested = new Subject<WeValidationRequestedEventArgs>();
        private readonly ISubject<WeValidationStateChangedEventArgs> _onValidationStateChanged = new Subject<WeValidationStateChangedEventArgs>();
        private readonly ISubject<WeValidationStateChangedEventArgs> _OnFormValid = new Subject<WeValidationStateChangedEventArgs>();
        public IObservable<WeFieldChangedEventArgs> OnFieldChanged() => _onFieldChanged.AsObservable();
        public IObservable<WeValidationRequestedEventArgs> OnValidationRequested() => _onValidationRequested.AsObservable();
        public IObservable<WeValidationStateChangedEventArgs> OnValidationStateChanged() => _onValidationStateChanged.AsObservable();

        public IObservable<WeValidationStateChangedEventArgs> OnFormValid() => _OnFormValid.AsObservable();
        internal WeEditContext(IWeForm parentForm, TModel model)
        {
            Console.WriteLine($"Create {nameof(WeEditContext<TModel>)}");
            ParentForm = parentForm;
            Model = model;
        }

        /// <summary>
        /// Gets the model object for this <see cref="WeEditContext"/>.
        /// </summary>
        public TModel Model { get; }

        public object GetModel() => Model;

        public Type GetModelType() => typeof(TModel);

        public IWeForm ParentForm { get; }

        public bool IsValid() => !GetValidationMessages().Any();

        public bool IsValid(WeFieldIdentifier fieldIdentifier) => !GetValidationMessages(fieldIdentifier).Any();

        /// <summary>
        /// Supplies a <see cref="FieldIdentifier"/> corresponding to a specified field name
        /// on this <see cref="EditContext"/>'s <see cref="Model"/>.
        /// </summary>
        /// <param name="fieldName">The name of the editable field.</param>
        /// <returns>A <see cref="FieldIdentifier"/> corresponding to a specified field name on this <see cref="EditContext"/>'s <see cref="Model"/>.</returns>
        public WeFieldIdentifier Field(string fieldName) => new WeFieldIdentifier(Model, fieldName, Model.IsFieldRequired(fieldName));


        /// <summary>
        /// Signals that the value for the specified field has changed.
        /// </summary>
        /// <param name="fieldIdentifier">Identifies the field whose value has been changed.</param>
        public void NotifyFieldChanged(in WeFieldIdentifier fieldIdentifier)
        {
            Console.WriteLine($"NotifyfieldChanged:{fieldIdentifier.FieldName}");
            GetFieldState(fieldIdentifier, ensureExists: true).IsModified = true;
            _onFieldChanged.OnNext(new WeFieldChangedEventArgs(fieldIdentifier));

            NotifyFormValid();
        }

        public void NotifyFormValid()
        {
            if (IsValid())
                _OnFormValid.OnNext(WeValidationStateChangedEventArgs.Empty);
        }
        /// <summary>
        /// Signals that some aspect of validation state has changed.
        /// </summary>
        public void NotifyValidationStateChanged() => _onValidationStateChanged.OnNext(WeValidationStateChangedEventArgs.Empty);

        /// <summary>
        /// Signals that some aspect of validation state has changed.
        /// </summary>
        public void NotifyValidationStateChanged(in WeFieldIdentifier fieldIdentifier) => _onValidationStateChanged.OnNext(WeValidationStateChangedEventArgs.Create(fieldIdentifier));

        /// <summary>
        /// Clears any modification flag that may be tracked for the specified field.
        /// </summary>
        /// <param name="fieldIdentifier">Identifies the field whose modification flag (if any) should be cleared.</param>
        public void MarkAsUnmodified(in WeFieldIdentifier fieldIdentifier)
        {
            if (_fieldStates.TryGetValue(fieldIdentifier, out var state))
            {
                state.IsModified = false;
            }
        }

        /// <summary>
        /// Clears all modification flags within this <see cref="EditContext"/>.
        /// </summary>
        public void MarkAsUnmodified()
        {
            foreach (var state in _fieldStates)
            {
                state.Value.IsModified = false;
            }
        }

        /// <summary>
        /// Determines whether any of the fields in this <see cref="EditContext"/> have been modified.
        /// </summary>
        /// <returns>True if any of the fields in this <see cref="EditContext"/> have been modified; otherwise false.</returns>
        public bool IsModified() => _fieldStates.Where(state => state.Value.IsModified).Any();

        /// <summary>
        /// Gets the current validation messages across all fields.
        ///
        /// This method does not perform validation itself. It only returns messages determined by previous validation actions.
        /// </summary>
        /// <returns>The current validation messages.</returns>
        public IEnumerable<string> GetValidationMessages()
        {
            // Since we're only enumerating the fields for which we have a non-null state, the cost of this grows
            // based on how many fields have been modified or have associated validation messages
            foreach (var state in _fieldStates)
            {
                foreach (var message in state.Value.GetValidationMessages())
                {
                    yield return message;
                }
            }
        }

        /// <summary>
        /// Gets the current validation messages for the specified field.
        ///
        /// This method does not perform validation itself. It only returns messages determined by previous validation actions.
        /// </summary>
        /// <param name="fieldIdentifier">Identifies the field whose current validation messages should be returned.</param>
        /// <returns>The current validation messages for the specified field.</returns>
        public IEnumerable<string> GetValidationMessages(WeFieldIdentifier fieldIdentifier)
        {
            if (_fieldStates.TryGetValue(fieldIdentifier, out var state))
            {
                foreach (var message in state.GetValidationMessages())
                {
                    yield return message;
                }
            }
        }

        /// <summary>
        /// Gets the current validation messages for the specified field.
        ///
        /// This method does not perform validation itself. It only returns messages determined by previous validation actions.
        /// </summary>
        /// <param name="accessor">Identifies the field whose current validation messages should be returned.</param>
        /// <returns>The current validation messages for the specified field.</returns>
        public IEnumerable<string> GetValidationMessages(Expression<Func<object>> accessor)
            => GetValidationMessages(WeFieldIdentifier.Create(accessor));

        /// <summary>
        /// Determines whether the specified fields in this <see cref="EditContext"/> has been modified.
        /// </summary>
        /// <returns>True if the field has been modified; otherwise false.</returns>
        public bool IsModified(in WeFieldIdentifier fieldIdentifier)
            => _fieldStates.TryGetValue(fieldIdentifier, out var state)
            ? state.IsModified
            : false;

        /// <summary>
        /// Determines whether the specified fields in this <see cref="EditContext"/> has been modified.
        /// </summary>
        /// <param name="accessor">Identifies the field whose current validation messages should be returned.</param>
        /// <returns>True if the field has been modified; otherwise false.</returns>
        public bool IsModified(Expression<Func<object>> accessor)
            => IsModified(WeFieldIdentifier.Create(accessor));

        /// <summary>
        /// Validates this <see cref="EditContext"/>.
        /// </summary>
        /// <returns>True if there are no validation messages after validation; otherwise false.</returns>
        public bool Validate()
        {
            _onValidationRequested.OnNext(WeValidationRequestedEventArgs.Empty);
            return !GetValidationMessages().Any();
        }

        /// <summary>
        /// Validates this <see cref="EditContext"/>.
        /// </summary>
        /// <returns>True if there are no validation messages after validation; otherwise false.</returns>
        public bool Validate(WeFieldIdentifier fieldIdentifier)
        {
            _onValidationRequested.OnNext(WeValidationRequestedEventArgs.Create(fieldIdentifier));
            return !GetValidationMessages(fieldIdentifier).Any();
        }

        public WeFieldState GetFieldState(in WeFieldIdentifier fieldIdentifier, bool ensureExists)

        {
            if (!_fieldStates.TryGetValue(fieldIdentifier, out var state) && ensureExists)
            {
                state = new WeFieldState(fieldIdentifier);
                _fieldStates.Add(fieldIdentifier, state);
            }

            return state;
        }



        public void Dispose()
        {

        }
    }

}
