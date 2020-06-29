using System.ComponentModel;

namespace WeStrap
{
    public enum WeStepperStatus
    {
        None,
        [Description("active")]
        Active,
        [Description("wrong")]
        Wrong,
        [Description("completed")]
        Completed
    }
}
