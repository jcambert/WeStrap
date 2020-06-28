using System.ComponentModel;

namespace WeStrap
{
    public enum WeStepperType
    {
        [Description("stepper-horizontal")]
        HorizontalLinear,

        [Description("stepper-horizontal")]
        HorizontalNonLinear,

        [Description("stepper-vertical")]
        VerticalLinear,

        [Description("stepper-vertical")]
        VerticalNonLinear,

        [Description("horizontal")]
        HorizontalContentLinear

    }
}
