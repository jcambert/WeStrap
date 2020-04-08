using System;

namespace WeStrap
{
    /// <summary>
    /// Provides information about the <see cref="EditContext.OnValidationRequested"/> event.
    /// </summary>
    public sealed class WeValidationRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a shared empty instance of <see cref="WeValidationRequestedEventArgs"/>.
        /// </summary>
        public static new readonly WeValidationRequestedEventArgs Empty = new WeValidationRequestedEventArgs();
        public static WeValidationRequestedEventArgs Create(WeFieldIdentifier args) => new WeValidationRequestedEventArgs(args);

        public WeFieldIdentifier Field { get; private set; }


        /// <summary>
        /// Creates a new instance of <see cref="WeValidationRequestedEventArgs"/>.
        /// </summary>
        private WeValidationRequestedEventArgs()
        {
            Field = WeFieldIdentifier.Empty;
        }

        public WeValidationRequestedEventArgs(WeFieldIdentifier fieldIdentifier)
        {
            Field = fieldIdentifier;
        }
    }
}
