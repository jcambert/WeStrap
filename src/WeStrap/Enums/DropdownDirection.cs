using System.ComponentModel;

namespace WeStrap
{
    public enum DropdownDirection
    {
        Down,
        [Description("dropup")]
        Up,
        [Description("dropright")]
        Right,
        [Description("dropleft")]
        Left
    }
}
