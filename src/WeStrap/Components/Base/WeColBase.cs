using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public class WeColBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add(Offsets.ToDescriptionString())
                .Add(Columns.ToDescriptionString())
                .Add("col", Fill)
                ;
        }

        [Parameter] public IReadOnlyList<WeOffset> Offsets { get; set; } = new List<WeOffset>();
        [Parameter] public IReadOnlyList<WeColumn> Columns { get; set; } = new List<WeColumn>();
        [Parameter] public bool Fill { get; set; } = false;


    }
}
