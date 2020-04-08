using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public abstract class WeComponentBase : ComponentBase
    {
        // [Parameter] public string id { get; set; }
        //[Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public string Class { get; set; } = string.Empty;

        protected string ClassName => BuildClassName().ToString();

        protected virtual WeStringBuilder BuildClassName(string s = "")
        {
            return new WeStringBuilder().Add(s).Add(Class);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

        protected static string GenerateId() => Guid.NewGuid().ToString().Replace("-", "");
    }
}
