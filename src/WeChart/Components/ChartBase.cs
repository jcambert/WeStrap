using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace WeChart
{
    public class ChartBase : ComponentBase
    {
        public ElementReference Me { get; protected set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        [Parameter] public string DefaultChartType { get; set; }
        [Parameter] public IEnumerable<string> Labels { get; set; } = new List<string>();

        [Parameter] public IOptions Options { get; set; }
        [Inject] public IRegistry Registry { get; set; }
    }
}
