using Microsoft.AspNetCore.Components;

namespace WeStrap
{
    public interface IWeEditContextBase
    {
        IWeEditContext CascadedEditContext { get; set; }
    }
    public abstract class WeEditContextBase : ComponentBase, IWeEditContextBase
    {
        [CascadingParameter] public IWeEditContext CascadedEditContext { get; set; }
    }
}
