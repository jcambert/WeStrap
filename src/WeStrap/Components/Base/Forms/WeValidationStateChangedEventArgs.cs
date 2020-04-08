using System;

namespace WeStrap
{
    /// <summary>
    /// Provides information about the <see cref="EditContext.OnValidationStateChanged"/> event.
    /// </summary>
    public sealed class WeValidationStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a shared empty instance of <see cref="ValidationStateChangedEventArgs"/>.
        /// </summary>
        public new static readonly WeValidationStateChangedEventArgs Empty = new WeValidationStateChangedEventArgs();

        public static WeValidationStateChangedEventArgs Create(WeFieldIdentifier field) => new WeValidationStateChangedEventArgs(field);
        /// <summary>
        /// Creates a new instance of <see cref="WeValidationStateChangedEventArgs" />
        /// </summary>
        private WeValidationStateChangedEventArgs(WeFieldIdentifier fieldIdentifier)
        {
            this.Field = fieldIdentifier;
        }
        public WeValidationStateChangedEventArgs()
        {
            Field = WeFieldIdentifier.Empty;
        }

        public WeFieldIdentifier Field { get; private set; }
    }
}
