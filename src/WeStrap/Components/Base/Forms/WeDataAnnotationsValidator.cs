using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;

namespace WeStrap
{
    public class WeDataAnnotationsValidator : WeEditContextBase
    {


        /// <inheritdoc />
        protected override void OnInitialized()
        {

            CascadedEditContext?.AddDataAnnotationsValidation();
        }
    }

    internal static class EditContextDataAnnotationsExtensions
    {
        private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache
            = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        /// <summary>
        /// Adds DataAnnotations validation support to the <see cref="EditContext"/>.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/>.</param>
        internal static IWeEditContext AddDataAnnotationsValidation(this IWeEditContext editContext)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            var messages = WeValidationMessageStore.Create(editContext);



            // Perform object-level validation on request
            editContext.OnValidationRequested().Where(args => args.Field == WeFieldIdentifier.Empty).Subscribe(args =>
            {
                //Console.WriteLine($"DataAnnotationValidator OnValidationRequested on Model");
                ValidateModel(editContext, messages);
            });

            editContext.OnValidationRequested().Where(args => args.Field != WeFieldIdentifier.Empty).Subscribe(args =>
            {
               // Console.WriteLine($"DataAnnotationValidator OnValidationRequested on field:{args.Field.FieldName}");
                ValidateField(editContext, messages, args.Field);
            });

            // Perform per-field validation on each field edit
            editContext.OnFieldChanged().Subscribe(args =>
            {
                //Console.WriteLine($"DataAnnotationValidator OnfieldChanged->ValidateField:{args.FieldIdentifier.FieldName}");
                ValidateField(editContext, messages, args.FieldIdentifier);
            });



            return editContext;
        }

        private static void ValidateModel(IWeEditContext editContext, IWeValidationMessageStore messages)
        {
            var validationContext = new ValidationContext(editContext.GetModel());
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(editContext.GetModel(), validationContext, validationResults, true);

            // Transfer results to the ValidationMessageStore
            messages.Clear();
            foreach (var validationResult in validationResults)
            {
                if (!validationResult.MemberNames.Any())
                {
                    messages.Add(new WeFieldIdentifier(editContext.GetModel(), fieldName: string.Empty, false), validationResult.ErrorMessage);
                    continue;
                }

                foreach (var memberName in validationResult.MemberNames)
                {
                    messages.Add(editContext.Field(memberName), validationResult.ErrorMessage);
                }
            }

            editContext.NotifyValidationStateChanged();
        }

        private static void ValidateField(IWeEditContext editContext, IWeValidationMessageStore messages, in WeFieldIdentifier fieldIdentifier)
        {
           // Console.WriteLine($"DataAnnotationValidator start ValidateField:{fieldIdentifier.FieldName}");
            if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
            {
                var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
                var validationContext = new ValidationContext(fieldIdentifier.Model)
                {
                    MemberName = propertyInfo.Name
                };
                var results = new List<ValidationResult>();

                Validator.TryValidateProperty(propertyValue, validationContext, results);
                messages.Clear(fieldIdentifier);
                messages.Add(fieldIdentifier, results.Select(result => result.ErrorMessage));

                //Console.WriteLine($"DataAnnotationValidator Validated with {messages.Count(fieldIdentifier)} Error(s)");
                // We have to notify even if there were no messages before and are still no messages now,
                // because the "state" that changed might be the completion of some async validation task
                editContext.NotifyValidationStateChanged(fieldIdentifier);
            }
            //Console.WriteLine($"DataAnnotationValidator end ValidateField:{fieldIdentifier.FieldName}");
        }

        private static bool TryGetValidatableProperty(in WeFieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // DataAnnotations only validates public properties, so that's all we'll look for
                // If we can't find it, cache 'null' so we don't have to try again next time
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                // No need to lock, because it doesn't matter if we write the same value twice
                _propertyInfoCache[cacheKey] = propertyInfo;
            }

            return propertyInfo != null;
        }
    }
}
