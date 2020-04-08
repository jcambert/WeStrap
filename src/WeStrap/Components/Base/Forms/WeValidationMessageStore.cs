using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeStrap
{
    public class WeValidationMessageStore
    {
        internal static IWeValidationMessageStore Create(IWeEditContext ctx)
        {
            var _modelType = ctx.ParentForm.ModelType;
            var _type = typeof(WeValidationMessageStore<>).MakeGenericType(_modelType);
            var result = Activator.CreateInstance(_type, new object[] { ctx });

            return result as IWeValidationMessageStore;
        }
    }
    public interface IWeValidationMessageStore
    {
        void Clear();
        void Clear(Expression<Func<object>> accessor);

        void Clear(in WeFieldIdentifier fieldIdentifier);
        void Add(in WeFieldIdentifier fieldIdentifier, string message);

        void Add(Expression<Func<object>> accessor, string message);

        void Add(in WeFieldIdentifier fieldIdentifier, IEnumerable<string> messages);

        void Add(Expression<Func<object>> accessor, IEnumerable<string> messages);

        IEnumerable<string> this[WeFieldIdentifier fieldIdentifier] { get; }
        IEnumerable<string> this[Expression<Func<object>> accessor] { get; }

        int Count();

        int Count(in WeFieldIdentifier fieldIdentifier);
    }
    public sealed class WeValidationMessageStore<TModel> : IWeValidationMessageStore
        where TModel : class, new()
    {
        private readonly IWeEditContext _editContext;
        private readonly Dictionary<WeFieldIdentifier, List<string>> _messages = new Dictionary<WeFieldIdentifier, List<string>>();

        /// <summary>
        /// Creates an instance of <see cref="ValidationMessageStore"/>.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/> with which this store should be associated.</param>
        public WeValidationMessageStore(IWeEditContext editContext)
        {
            _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
        }

        /// <summary>
        /// Adds a validation message for the specified field.
        /// </summary>
        /// <param name="fieldIdentifier">The identifier for the field.</param>
        /// <param name="message">The validation message.</param>
        public void Add(in WeFieldIdentifier fieldIdentifier, string message)
            => GetOrCreateMessagesListForField(fieldIdentifier).Add(message);

        /// <summary>
        /// Adds a validation message for the specified field.
        /// </summary>
        /// <param name="accessor">Identifies the field for which to add the message.</param>
        /// <param name="message">The validation message.</param>
        public void Add(Expression<Func<object>> accessor, string message)
            => Add(WeFieldIdentifier.Create(accessor), message);

        /// <summary>
        /// Adds the messages from the specified collection for the specified field.
        /// </summary>
        /// <param name="fieldIdentifier">The identifier for the field.</param>
        /// <param name="messages">The validation messages to be added.</param>
        public void Add(in WeFieldIdentifier fieldIdentifier, IEnumerable<string> messages)
            => GetOrCreateMessagesListForField(fieldIdentifier).AddRange(messages);

        /// <summary>
        /// Adds the messages from the specified collection for the specified field.
        /// </summary>
        /// <param name="accessor">Identifies the field for which to add the messages.</param>
        /// <param name="messages">The validation messages to be added.</param>
        public void Add(Expression<Func<object>> accessor, IEnumerable<string> messages)
            => Add(WeFieldIdentifier.Create(accessor), messages);

        /// <summary>
        /// Gets the validation messages within this <see cref="ValidationMessageStore"/> for the specified field.
        ///
        /// To get the validation messages across all validation message stores, use <see cref="EditContext.GetValidationMessages(FieldIdentifier)"/> instead
        /// </summary>
        /// <param name="fieldIdentifier">The identifier for the field.</param>
        /// <returns>The validation messages for the specified field within this <see cref="ValidationMessageStore"/>.</returns>
        public IEnumerable<string> this[WeFieldIdentifier fieldIdentifier]
            => _messages.TryGetValue(fieldIdentifier, out var messages) ? messages : Enumerable.Empty<string>();

        /// <summary>
        /// Gets the validation messages within this <see cref="ValidationMessageStore"/> for the specified field.
        ///
        /// To get the validation messages across all validation message stores, use <see cref="EditContext.GetValidationMessages(FieldIdentifier)"/> instead
        /// </summary>
        /// <param name="accessor">The identifier for the field.</param>
        /// <returns>The validation messages for the specified field within this <see cref="ValidationMessageStore"/>.</returns>
        public IEnumerable<string> this[Expression<Func<object>> accessor]
            => this[WeFieldIdentifier.Create(accessor)];

        /// <summary>
        /// Removes all messages within this <see cref="ValidationMessageStore"/>.
        /// </summary>
        public void Clear()
        {
            foreach (var fieldIdentifier in _messages.Keys)
            {
                DissociateFromField(fieldIdentifier);
            }

            _messages.Clear();
        }

        /// <summary>
        /// Removes all messages within this <see cref="ValidationMessageStore"/> for the specified field.
        /// </summary>
        /// <param name="accessor">Identifies the field for which to remove the messages.</param>
        public void Clear(Expression<Func<object>> accessor)
            => Clear(WeFieldIdentifier.Create(accessor));

        /// <summary>
        /// Removes all messages within this <see cref="ValidationMessageStore"/> for the specified field.
        /// </summary>
        /// <param name="fieldIdentifier">The identifier for the field.</param>
        public void Clear(in WeFieldIdentifier fieldIdentifier)
        {
            DissociateFromField(fieldIdentifier);
            _messages.Remove(fieldIdentifier);
        }

        public int Count()
        {
            var result = 0;
            foreach (var item in _messages)
            {
                foreach (var item1 in item.Value)
                {
                    result += item1.Count();
                }
            }
            return result;
        }

        public int Count(in WeFieldIdentifier fieldIdentifier) => _messages[fieldIdentifier].Count;

        private List<string> GetOrCreateMessagesListForField(in WeFieldIdentifier fieldIdentifier)
        {
            if (!_messages.TryGetValue(fieldIdentifier, out var messagesForField))
            {
                messagesForField = new List<string>();
                _messages.Add(fieldIdentifier, messagesForField);
                AssociateWithField(fieldIdentifier);
            }

            return messagesForField;
        }

        private void AssociateWithField(in WeFieldIdentifier fieldIdentifier)
            => _editContext.GetFieldState(fieldIdentifier, ensureExists: true).AssociateWithValidationMessageStore(this);

        private void DissociateFromField(in WeFieldIdentifier fieldIdentifier)
            => _editContext.GetFieldState(fieldIdentifier, ensureExists: false)?.DissociateFromValidationMessageStore(this);
    }
}
