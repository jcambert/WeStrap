using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using WeCommon;
namespace WeStrap
{
    public class ItemWithIndex<TItem>
    {
        public TItem Item { get; set; }
        public int Index { get; set; }
    }
    public abstract class WeTableBase<TItem> : WeTag, IHeadCells
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("table")
            .Add("table-dark", IsDark)
            .Add("table-striped", IsStriped)
            .Add("table-bordered", IsBordered)
            .Add("table-borderless", IsBorderless)
            .Add("table-hover", IsHovarable)
            .Add("table-sm", IsSmall);
        }
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public bool IsBordered { get; set; }
        [Parameter] public bool IsBorderless { get; set; }
        [Parameter] public bool IsHovarable { get; set; }
        [Parameter] public bool IsSmall { get; set; }
        [Parameter] public bool IsResponsive { get; set; }
        [Parameter] public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();
        [Parameter] public RenderFragment THead { get; set; }
        [Parameter] public RenderFragment<ItemWithIndex<TItem>> TBody { get; set; }
        [Parameter] public RenderFragment<object> TGroup { get; set; }
        [Parameter] public RenderFragment<object> TBodyGroup { get; set; }
        [Parameter] public RenderFragment TFoot { get; set; }
        [Parameter] public TableHeadType TableHeadType { get; set; } = TableHeadType.None;
        [Parameter] public bool UseHeadCellColor { get; set; } = false;


        [Parameter] public IReadOnlyList<string> HeadCellsSimple { get; set; }
        [Parameter] public IList<TableHeadCell> HeadCells { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if ((HeadCellsSimple?.Any() ?? false) && (HeadCells?.Any() ?? false))
                throw new ArgumentException("HeadCellsSimple and HeadCells cannot be used together");
            if (HeadCellsSimple?.Any() ?? false)
            {
                HeadCells = new List<TableHeadCell>();
                foreach (var item in HeadCellsSimple)
                {
                    HeadCells.Add(new TableHeadCell() { Head = item });
                }
            }
        }

    }
}
