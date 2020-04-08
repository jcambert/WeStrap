using System;

namespace WeStrap
{
    public sealed class WeFieldChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="FieldChangedEventArgs"/>.
        /// </summary>
        /// <param name="fieldIdentifier">The <see cref="Forms.FieldIdentifier"/></param>
        public WeFieldChangedEventArgs(in WeFieldIdentifier fieldIdentifier)
        {
            FieldIdentifier = fieldIdentifier;
        }

        /// <summary>
        /// Identifies the field whose value has changed.
        /// </summary>
        public WeFieldIdentifier FieldIdentifier { get; }
    }
}
