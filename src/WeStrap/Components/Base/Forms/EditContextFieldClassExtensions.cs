using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WeStrap
{
    /// <summary>
    /// Provides extension methods to describe the state of <see cref="EditContext"/>
    /// fields as CSS class names.
    /// </summary>
    public static class EditContextFieldClassExtensions
    {
        /// <summary>
        /// Gets a string that indicates the status of the specified field as a CSS class. This will include
        /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/>.</param>
        /// <param name="accessor">An identifier for the field.</param>
        /// <returns>A string that indicates the status of the field.</returns>
        public static string FieldCssClass<TField>(this IWeEditContext editContext, Expression<Func<TField>> accessor)
            => FieldCssClass(editContext, WeFieldIdentifier.Create(accessor));

        /// <summary>
        /// Gets a string that indicates the status of the specified field as a CSS class. This will include
        /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/>.</param>
        /// <param name="fieldIdentifier">An identifier for the field.</param>
        /// <returns>A string that indicates the status of the field.</returns>
        public static string FieldCssClass(this IWeEditContext editContext, in WeFieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
            if (editContext.IsModified(fieldIdentifier))
            {
                return isValid ? "modified valid" : "modified invalid";
            }
            else
            {
                return isValid ? "valid" : "invalid";
            }
        }
    }
}
