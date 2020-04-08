using Microsoft.AspNetCore.Components;
using WeCommon;
namespace WeStrap
{
    public abstract class WeTableHeadCellBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                 .Add($"table-{CellColor}", HasCellColor);
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
        [Parameter] public int ColIndex { get; set; }
    }
}
