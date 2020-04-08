using Microsoft.AspNetCore.Components;
using System;
using WeCommon;

namespace WeStrap
{
    public partial class WeForm<TModel> : WeFormBase<TModel>
        where TModel : class, new()
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName()
                 .Add("form-inline", IsInline)
                 .Add("was-validated", EditContext?.IsValid() ?? false)
                 ;
        }

        [Parameter] public bool IsInline { get; set; }


        [Parameter] public RenderFragment<TModel> Template { get; set; }
    }
}
