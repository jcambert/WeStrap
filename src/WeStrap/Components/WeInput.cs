using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using WeCommon;


namespace WeStrap
{
    public class WeInput<TValue> : WeInputBase<TValue>
    {
        private bool _clean = true;
        private bool _touched = false;





        protected override WeStringBuilder BuildClassName(string s = "")
        {
            var result = base.BuildClassName(s)
            .Add("form-control", Size == Size.None)
           .Add($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
           .Add("is-valid", IsValid && IsRequired )
           .Add("is-invalid", IsInvalid && IsRequired );
          //  Console.WriteLine($"{Tag}-{FieldIdentifier.FieldName} - Class={result}");
            return result;
        }

        protected bool HasValidationErrors()
        {
            if (_clean || CascadedEditContext == null)
            {
                _clean = false;
                return false;
            }
            return CascadedEditContext.GetValidationMessages(base.FieldIdentifier).Any();
        }

        protected override string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };

        
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string MaxDate { get; set; } = "9999-12-31";
        [Parameter] public virtual TValue RadioValue { get; set; }
        [Parameter] public bool IsReadonly { get; set; }
        [Parameter] public bool IsPlaintext { get; set; }
        [Parameter] public bool IsChecked { get; set; }
        public override bool IsValid => CascadedEditContext?.IsValid(FieldIdentifier) ?? true;
        public override bool IsInvalid => !IsValid;
        public override bool IsRequired => FieldIdentifier.IsRequired;
        [Parameter] public bool IsMultipleSelect { get; set; }
        [Parameter] public int? SelectSize { get; set; }
        [Parameter] public int? SelectedIndex { get; set; }
        [Parameter] public bool ForceUpperCase { get; set; } = false;
        [Parameter] public bool ForceTitleCase { get; set; } = false;

        protected string Type => InputType.ToDescriptionString();

        private const string _dateFormat = "yyyy-MM-dd";

        //private IDisposable _OnvalidationRequest;


        protected override void OnInitialized()
        {
            base.OnInitialized();
            // _OnvalidationRequest=CascadedEditContext?.OnValidationRequested().Subscribe(args => { _touched = true; });
            CascadedEditContext?.OnValidationStateChanged()
                .Where(args => args.Field == FieldIdentifier)
                .Subscribe(args =>
                {
                   // ValidationWasMade = true;
                    StateHasChanged();
                });
            //CascadedEditContext?.OnFormValid().Subscribe(args => ValidationWasMade = true);
        }




        private string GetClass()
        {
            return InputType switch
            {
                InputType.Checkbox => "form-check-input",
                InputType.Radio => "form-check-input",
                InputType.File => "form-control-file",
                InputType.Range => "form-control-range",
                _ => IsPlaintext ? "form-control-plaintext" : "form-control"
            };
        }

        protected void OnClick(MouseEventArgs e)
        {
            if (InputType == InputType.Radio)
            {
                Value = (TValue)(object)(RadioValue);
                ValueChanged.InvokeAsync(Value);
            }
            else
            {
                var tmp = (bool)(object)Value;
                Value = (TValue)(object)(!tmp);
                ValueChanged.InvokeAsync(Value);
            }
        }

        protected void OnChange(string e)
        {
            //ValidationWasMade = false;
           //Console.WriteLine("*******************************");
            Console.WriteLine($"OnChangeTo:{e}");

            CurrentValueAsString = FormatInput?.Invoke(e) ?? e;
        }

        protected void OnInput(string e)
        {
            //ValidationWasMade = false;
          //  Console.WriteLine("*******************************");
            Console.WriteLine($"OnInputTo:{e}");

            CurrentValueAsString = FormatInput?.Invoke(e) ?? e;
        }


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (CascadedEditContext?.ParentForm != null)
                CascadedEditContext.ParentForm.AddInputChild(this);
          //  Console.WriteLine("WeInout->OnAfterRender");
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

         //   Console.WriteLine("WeInput->BuildRenderTree");
            builder?.OpenElement(0, Tag);
            builder.AddMultipleAttributes(1, UnknownParameters);
            builder.AddAttribute(2, "class", ClassName);
            builder.AddAttribute(3, "type", Type);
            builder.AddAttribute(4, "readonly", IsReadonly);
            builder.AddAttribute(5, "disabled", IsDisabled);
            builder.AddAttribute(6, "multiple", IsMultipleSelect);
            builder.AddAttribute(7, "size", SelectSize);
            builder.AddAttribute(8, "selectedIndex", SelectedIndex);
            if (InputType == InputType.Checkbox)
            {
                builder.AddAttribute(9, "checked", BindConverter.FormatValue(CurrentValue));
                builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
            }
            else if (InputType == InputType.Radio)
            {
                if (RadioValue.Equals(Value))
                {
                    builder.AddAttribute(9, "checked", true);
                    builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
                }
                else
                {
                    builder.AddAttribute(9, "checked", false);
                    builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
                }
            }
            else
            {
                builder.AddAttribute(9, "value", BindConverter.FormatValue(CurrentValueAsString));
                if (ValidateOnChange)
                    builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
                if(ValidateOnInput)
                    builder.AddAttribute(11, "oninput", EventCallback.Factory.CreateBinder<string>(this, OnInput, CurrentValueAsString));
               

                if (InputType == InputType.Date && !String.IsNullOrEmpty(MaxDate))
                {
                    builder.AddAttribute(13, "max", MaxDate);
                }
            }
            builder.AddAttribute(14, "tabindex", TabIndex);
            //builder.AddAttribute(12, "onblur", EventCallback.Factory.Create(this, () => { _touched = true; ValidateField(base.FieldIdentifier); }));

            //builder.AddAttribute(13, IsRequired ? "required" : "not-required","");
            builder.AddAttribute(15, "id", Id);
            builder.AddContent(16, ChildContent);
            builder.CloseElement();
        }

        protected override string FormatValueAsString(TValue value)
        {
            return value switch
            {
                null => null,
                int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
                long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
                float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
                double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
                decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
                DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _dateFormat, CultureInfo.InvariantCulture),
                DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _dateFormat, CultureInfo.InvariantCulture),
                _ => base.FormatValueAsString(value),
            };
        }

        public void ForceValidate()
        {
            CascadedEditContext?.Validate();

        }

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            Type type = typeof(TValue);
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (value == null && (Nullable.GetUnderlyingType(type) != null))
            {
                result = (TValue)(object)default(TValue);
                validationErrorMessage = null;
                return true;
            }
            else if (value?.Length == 0 && typeof(DateTime) != typeof(TValue) && typeof(DateTimeOffset) != typeof(TValue))
            {
                result = (TValue)(object)default(TValue);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue).IsEnum)
            {
                // There's no non-generic Enum.TryParse (https://github.com/dotnet/corefx/issues/692)
                try
                {
                    result = (TValue)Enum.Parse(typeof(TValue), value);
                    validationErrorMessage = null;
                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            else if (typeof(TValue) == typeof(int) || typeof(TValue) == typeof(int?))
            {
                result = (TValue)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue) == typeof(long) || typeof(TValue) == typeof(long?))
            {
                result = (TValue)(object)Convert.ToInt64(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue) == typeof(double) || typeof(TValue) == typeof(double?))
            {
                result = (TValue)(object)double.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue) == typeof(decimal) || typeof(TValue) == typeof(decimal?))
            {
                result = (TValue)(object)decimal.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue) == typeof(Guid) || typeof(TValue) == typeof(Guid?))
            {
                try
                {
                    result = (TValue)(object)Guid.Parse(value);
                }
                catch
                {
                    result = (TValue)(object)new Guid();
                    validationErrorMessage = "Invalid Guid format";
                }
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue) == typeof(DateTime) || typeof(TValue) == typeof(DateTime?))
            {
                if (TryParseDateTime(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", FieldIdentifier.FieldName);
                    return false;
                }
            }
            else if (typeof(TValue) == typeof(DateTimeOffset) || typeof(TValue) == typeof(DateTimeOffset?))
            {
                if (TryParseDateTimeOffset(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", FieldIdentifier.FieldName);
                    return false;
                }
            }
            else if (type.GenericTypeArguments.Length > 0 && type.GenericTypeArguments[0].IsEnum)
            {
                try
                {
                    result = (TValue)Enum.Parse(type.GenericTypeArguments[0].UnderlyingSystemType, value);
                    validationErrorMessage = null;
                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
        }

        private static bool TryParseDateTime(string value, out TValue result)
        {
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, _dateFormat, out DateTime parsedValue);
            if (success)
            {
                result = (TValue)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        private static bool TryParseDateTimeOffset(string value, out TValue result)
        {
            var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.InvariantCulture, _dateFormat, out DateTimeOffset parsedValue);
            if (success)
            {
                result = (TValue)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        private void ValidateField(WeFieldIdentifier fieldIdentifier)
        {
            if (ValidateOnChange)
                CascadedEditContext?.NotifyFieldChanged(fieldIdentifier);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //_OnvalidationRequest?.Dispose();
        }
    }
}
