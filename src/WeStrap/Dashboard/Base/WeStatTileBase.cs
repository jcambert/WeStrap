using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using WeCommon;
namespace WeStrap
{
    public abstract class WeStatTileBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName().UsePrefix(CLASS_PREFIX).Add("tile_stat_count").RemovePrefix();
        }
        [Parameter] public IReadOnlyList<WeColumn> Columns { get; set; } = new List<WeColumn>() { WeColumn.md2, WeColumn.sm4 };
        [Parameter] public RenderFragment Top { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public RenderFragment Bottom { get; set; }
    }
}
