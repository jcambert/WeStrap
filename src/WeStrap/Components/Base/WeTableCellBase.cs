using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeTableCellBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add($"table-{CellColor}", HasCellColor)
                .Add($"text-{Alignment.ToDescriptionString()}", Alignment != Alignment.None)
                .Add("align-top", VerticalAlignment == VerticalAlignment.Top)
                .Add("align-middle", VerticalAlignment == VerticalAlignment.Center)
                .Add("align-bottom", VerticalAlignment == VerticalAlignment.Bottom)
                ;
        }

        private bool HasCellColor => !string.IsNullOrEmpty(CellColor);
        private string CellColor
        {
            get
            {
                if (Parent?.UseHeadCellColor ?? false)
                    return Parent.HeadCells[ColIndex].Color.ToDescriptionString();
                return Color.ToDescriptionString();
            }
        }
        [CascadingParameter]
        public IHeadCells Parent
        {
            get;
            set;
        }


        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public int RowIndex { get; set; }
        [Parameter] public int ColIndex { get; set; }
        [Parameter] public EventCallback<WeEvent<WeTableCell>> OnCellClick { get; set; }
        [Parameter] public Alignment Alignment { get; set; }

        [Parameter] public VerticalAlignment VerticalAlignment { get; set; }
    }
}
