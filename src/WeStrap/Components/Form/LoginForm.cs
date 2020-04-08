using Microsoft.AspNetCore.Components;

namespace WeStrap
{
    public partial class LoginForm<TModel>
        where TModel : class, new()
    {
        [Parameter] public TModel Value { get; set; }

    }
}
