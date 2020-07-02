using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace WeStrap
{
    public interface IWeInputBase
    {
        string Id { get; }
        int TabIndex { get; set; }
        bool IsValid { get; }
        bool IsInvalid { get; }
        bool IsRequired { get; }
    }
    /// <summary>
    /// A base class for form input components. This base class automatically
    /// integrates with an <see cref="Forms.EditContext"/>, which must be supplied
    /// as a cascading parameter.
    /// </summary>
    public abstract class WeInputBase<TValue> : WeTag, IWeInputBase, IDisposable
    {

        private bool _previousParsingAttemptFailed;
        private IWeValidationMessageStore _parsingValidationMessages;
        private Type _nullableUnderlyingType;
        private IDisposable _onValidationStatechanged;

        [Inject] public StringService StringSvc { get; set; }

        [CascadingParameter] protected IWeEditContext CascadedEditContext { get; set; }
        [CascadingParameter]protected WeStepperItemBase StepperItem { get; set; }
        protected IWeForm Parent => CascadedEditContext?.ParentForm;

        [Parameter] public bool ValidateOnChange { get; set; } = true;
        [Parameter] public bool ValidateOnInput { get; set; } = false;

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter] public TValue Value { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter] public Expression<Func<TValue>> ValueExpression { get; set; }

        [Parameter] public int TabIndex { get; set; }

        public string Id => Guid.NewGuid().ToString().Replace("-", "");
        public abstract bool IsValid { get; }
        public abstract bool IsInvalid { get; }
        public abstract bool IsRequired { get; }



        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected WeFieldIdentifier FieldIdentifier { get; set; }

        protected Func<string, string> FormatInput { get; set; } = s => s;
        /// <summary>
        /// Gets or sets the current value of the input.
        /// </summary>
        protected TValue CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
                if (hasChanged)
                {
                   // Console.WriteLine($"changed Current Value to {value}");
                    Value = value;

                    _ = ValueChanged.InvokeAsync(value);
                    if (this.ValidateOnChange || ValidateOnInput)
                    {
                        CascadedEditContext.NotifyFieldChanged(FieldIdentifier);
                    }
                }
                else
                {
                   // Console.WriteLine("Its same Current value ");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current value of the input, represented as a string.
        /// </summary>
        protected string CurrentValueAsString
        {
            get => FormatValueAsString(CurrentValue);
            set
            {
               // Console.WriteLine($"SetCurrentValueAsString:{value}");
                _parsingValidationMessages?.Clear();

                bool parsingFailed;

                if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
                {
                    // Assume if it's a nullable type, null/empty inputs should correspond to default(T)
                    // Then all subclasses get nullable support almost automatically (they just have to
                    // not reject Nullable<T> based on the type itself).
                    parsingFailed = false;
                    CurrentValue = default;
                }
                else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
                {
                   // Console.WriteLine($"Changing Current Value to {parsedValue}");
                    parsingFailed = false;
                    CurrentValue = parsedValue;
                }
                else
                {
                    parsingFailed = true;

                    if (_parsingValidationMessages == null)
                    {
                        _parsingValidationMessages = WeValidationMessageStore.Create(CascadedEditContext);// new WeValidationMessageStore(EditContext);
                    }

                    _parsingValidationMessages.Add(FieldIdentifier, validationErrorMessage);

                    // Since we're not writing to CurrentValue, we'll need to notify about modification from here
                    CascadedEditContext.NotifyFieldChanged(FieldIdentifier);
                }

                // We can skip the validation notification if we were previously valid and still are
                if (parsingFailed || _previousParsingAttemptFailed)
                {
                    CascadedEditContext.NotifyValidationStateChanged();
                    _previousParsingAttemptFailed = parsingFailed;
                }
            }
        }

        /// <summary>
        /// Constructs an instance of <see cref="InputBase{TValue}"/>.
        /// </summary>
        protected WeInputBase()
        {
            //   _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
        }

        /// <summary>
        /// Formats the value as a string. Derived classes can override this to determine the formating used for <see cref="CurrentValueAsString"/>.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A string representation of the value.</returns>
        protected virtual string FormatValueAsString(TValue value)
            => value?.ToString();

        /// <summary>
        /// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
        /// <see cref="CurrentValueAsString"/> interprets incoming values.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
        /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
        /// <returns>True if the value could be parsed; otherwise false.</returns>
        protected abstract bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage);

        /// <summary>
        /// Gets a string that indicates the status of the field being edited. This will include
        /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
        /// </summary>
        private string FieldClass => CascadedEditContext?.FieldCssClass(FieldIdentifier);

        /// <summary>
        /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
        /// properties. Derived components should typically use this value for the primary HTML element's
        /// 'class' attribute.
        /// </summary>
        protected string CssClass
        {
            get
            {
                if (UnknownParameters != null &&
                    UnknownParameters.TryGetValue("class", out var @class) &&
                    !string.IsNullOrEmpty(Convert.ToString(@class)))
                {
                    return $"{@class} {FieldClass}";
                }

                return FieldClass; // Never null or empty
            }
        }


        /// <inheritdoc />
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (CascadedEditContext != null)
            {


                if (ValueExpression == null)
                {
                    throw new InvalidOperationException($"{GetType()} requires a value for the 'ValueExpression' " +
                        $"parameter. Normally this is provided automatically when using 'bind-Value'.");
                }


                FieldIdentifier = WeFieldIdentifier.Create(ValueExpression);
                
                if (FieldIdentifier.IsUpperCase)
                    FormatInput = StringSvc.ToUpperCase;
                else if (FieldIdentifier.IslowerCase)
                    FormatInput = StringSvc.ToLowerCase;
                else if (FieldIdentifier.IsTitleCase)
                    FormatInput = StringSvc.ToTitleCase;
                else if (FieldIdentifier.IsCamelCase)
                    FormatInput = StringSvc.ToCamelCase;
                else if (FieldIdentifier.IsPascalCase)
                    FormatInput = StringSvc.ToPascalCase;


                // _onValidationStatechanged= CascadedEditContext.OnValidationStateChanged().Where(args=> args.Field==FieldIdentifier ). Subscribe(args => { StateHasChanged(); });// += _validationStateChangedHandler;
            }


            // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
            return base.SetParametersAsync(ParameterView.Empty);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (CascadedEditContext?.ParentForm?.ValidateOnInit ?? false)
                CascadedEditContext?.Validate(FieldIdentifier);
            
            StepperItem?.AddFieldIdentifier(FieldIdentifier);

        }
        protected virtual void Dispose(bool disposing)
        {
            _onValidationStatechanged?.Dispose();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}
