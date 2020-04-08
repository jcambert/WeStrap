using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace WeStrap
{
    public readonly struct WeFieldIdentifier : IEquatable<WeFieldIdentifier>
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldIdentifier"/> structure.
        /// </summary>
        /// <param name="model">The object that owns the field.</param>
        /// <param name="fieldName">The name of the editable field.</param>
        public WeFieldIdentifier(object model, string fieldName, bool isRequired,bool isTitleCase=false,bool isCamelCase=false,bool isPascalCase=false,bool isUpperCase=false,bool isLowerCase=false)
        {


            if (model.GetType().IsValueType)
            {
                throw new ArgumentException("The model must be a reference-typed object.", nameof(model));
            }

            Model = model ?? throw new ArgumentNullException(nameof(model)); ;

            // Note that we do allow an empty string. This is used by some validation systems
            // as a place to store object-level (not per-property) messages.
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));

            this.IsRequired = isRequired;
            this.IsTitleCase = isTitleCase;
            this.IsCamelCase = isCamelCase;
            this.IsPascalCase = isPascalCase;
            this.IslowerCase = isLowerCase;
            this.IsUpperCase = isUpperCase;

        }
        #endregion

        #region properties
        /// <summary>
        /// Gets the object that owns the editable field.
        /// </summary>
        public object Model { get; }

        /// <summary>
        /// Gets the name of the editable field.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Gets if the field Is Required
        /// </summary>
        public bool IsRequired { get; }

        /// <summary>
        /// Gets if the field must be Upper Cased
        /// </summary>
        public bool IsUpperCase { get; }

        /// <summary>
        /// Gets if the field must be Title Cased
        /// </summary>
        public bool IsTitleCase { get;  }

        /// <summary>
        /// Gets if the field must be Camel Cased
        /// </summary>
        public bool IsCamelCase { get;  }

        /// <summary>
        /// Get if the field must be lower Cased
        /// </summary>
        public bool IslowerCase { get;  }

        /// <summary>
        /// Get If the field must be Pascal Cased
        /// </summary>
        public bool IsPascalCase { get;  }
        #endregion

        #region IEquatable
        /// <inheritdoc />
        public override int GetHashCode()
        {
            // We want to compare Model instances by reference. RuntimeHelpers.GetHashCode returns identical hashes for equal object references (ignoring any `Equals`/`GetHashCode` overrides) which is what we want.
            var modelHash = RuntimeHelpers.GetHashCode(Model);
            var fieldHash = StringComparer.Ordinal.GetHashCode(FieldName);
            return (
                modelHash,
                fieldHash
            )
            .GetHashCode();
        }
        ///<inheritdoc />
        public override bool Equals(object obj)
            => obj is WeFieldIdentifier otherIdentifier
            && Equals(otherIdentifier);

        ///<inheritdoc />
        public bool Equals([AllowNull] WeFieldIdentifier other) =>
             ReferenceEquals(other.Model, Model) &&
            string.Equals(other.FieldName, FieldName, StringComparison.Ordinal)
            && other.IsRequired == IsRequired
            && other.IsCamelCase == IsCamelCase
            && other.IsPascalCase==IsPascalCase
            && other.IsTitleCase == IsTitleCase
            && other.IsUpperCase == IsUpperCase
            && other.IslowerCase == IslowerCase
            ;
        #endregion

        #region Utilities
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldIdentifier"/> structure.
        /// </summary>
        /// <param name="accessor">An expression that identifies an object member.</param>
        /// <typeparam name="TField">The field <see cref="Type"/>.</typeparam>
        internal static WeFieldIdentifier Create<TField>(Expression<Func<TField>> accessor)
        {
            if (accessor == null)
            {
                throw new ArgumentNullException(nameof(accessor));
            }

            ParseAccessor(accessor, out var model, out var fieldName, out var isRequired,out var isTitleCase,out var isCamelCase,out var isPascalCase,out var isUpperCase,out var isLowerCase);
            return new WeFieldIdentifier(model, fieldName, isRequired,isTitleCase,isCamelCase,isPascalCase,isUpperCase,isLowerCase);
        }

        internal static WeFieldIdentifier Empty = new WeFieldIdentifier();
        private static void ParseAccessor<T>(Expression<Func<T>> accessor, out object model, out string fieldName, out bool isRequired,out bool isTitleCase, out bool isCamelCase,out bool isPascalCase,out bool isUpperCase,out bool isLowerCase)
        {
            var accessorBody = accessor.Body;

            // Unwrap casts to object
            if (accessorBody is UnaryExpression unaryExpression
                && unaryExpression.NodeType == ExpressionType.Convert
                && unaryExpression.Type == typeof(object))
            {
                accessorBody = unaryExpression.Operand;
            }

            if (!(accessorBody is MemberExpression memberExpression))
            {
                throw new ArgumentException($"The provided expression contains a {accessorBody.GetType().Name} which is not supported. {nameof(WeFieldIdentifier)} only supports simple member accessors (fields, properties) of an object.");
            }

            // Identify the field name. We don't mind whether it's a property or field, or even something else.
            fieldName = memberExpression.Member.Name;
            isRequired = memberExpression.Member.GetCustomAttributes(typeof(ValidationAttribute), true).Any();
            isTitleCase = memberExpression.Member.GetCustomAttributes(typeof(TitleCaseAttribute), true).Any();
            isCamelCase = memberExpression.Member.GetCustomAttributes(typeof(CamelCaseAttribute), true).Any();
            isPascalCase = memberExpression.Member.GetCustomAttributes(typeof(PascalCaseAttribute), true).Any();
            isUpperCase = memberExpression.Member.GetCustomAttributes(typeof(UpperCaseAttribute), true).Any();
            isLowerCase = memberExpression.Member.GetCustomAttributes(typeof(LowerCaseAttribute), true).Any();
            // Get a reference to the model object
            // i.e., given an value like "(something).MemberName", determine the runtime value of "(something)",
            switch (memberExpression.Expression)
            {
                case ConstantExpression constantExpression:
                    model = constantExpression.Value;
                    break;
                default:
                    // It would be great to cache this somehow, but it's unclear there's a reasonable way to do
                    // so, given that it embeds captured values such as "this". We could consider special-casing
                    // for "() => something.Member" and building a cache keyed by "something.GetType()" with values
                    // of type Func<object, object> so we can cheaply map from "something" to "something.Member".
                    var modelLambda = Expression.Lambda(memberExpression.Expression);
                    var modelLambdaCompiled = (Func<object>)modelLambda.Compile();
                    model = modelLambdaCompiled();
                    break;
            }
        }

        public static bool operator ==(WeFieldIdentifier left, WeFieldIdentifier right)=>
            left.Equals(right);

        public static bool operator !=(WeFieldIdentifier left, WeFieldIdentifier right)=>
            !(left == right);
        
        #endregion
    }
}
