using System.Collections.Generic;

namespace WeStrap
{
    public sealed class WeFieldState
    {
        private readonly WeFieldIdentifier _fieldIdentifier;

        // We track which ValidationMessageStore instances have a nonempty set of messages for this field so that
        // we can quickly evaluate the list of messages for the field without having to query all stores. This is
        // relevant because each validation component may define its own message store, so there might be as many
        // stores are there are fields or UI elements.
        private HashSet<IWeValidationMessageStore> _validationMessageStores;

        public WeFieldState(WeFieldIdentifier fieldIdentifier)
        {
            _fieldIdentifier = fieldIdentifier;
        }

        public bool IsModified { get; set; }

        public IEnumerable<string> GetValidationMessages()
        {
            if (_validationMessageStores != null)
            {
                foreach (var store in _validationMessageStores)
                {
                    foreach (var message in store[_fieldIdentifier])
                    {
                        yield return message;
                    }
                }
            }
        }

        public void AssociateWithValidationMessageStore(IWeValidationMessageStore validationMessageStore)
        {
            if (_validationMessageStores == null)
            {
                _validationMessageStores = new HashSet<IWeValidationMessageStore>();
            }

            _validationMessageStores.Add(validationMessageStore);
        }

        public void DissociateFromValidationMessageStore(IWeValidationMessageStore validationMessageStore)
            => _validationMessageStores?.Remove(validationMessageStore);
    }
}
