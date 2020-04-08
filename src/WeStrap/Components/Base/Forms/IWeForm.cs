using System;

namespace WeStrap
{
    public interface IWeForm:IDisposable
    {
        IWeEditContext GetEditContext();
        Type ModelType { get; }
        bool UserValidation { get; set; }
        bool ValidateOnInit { get; set; }
        bool HasValidationErrors { get; }
        void ForceValidate();

        void AddInputChild(IWeInputBase component);
    }
}
