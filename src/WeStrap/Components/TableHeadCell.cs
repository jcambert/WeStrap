using System.Collections.Generic;

namespace WeStrap
{

    public interface IHeadCells
    {
        bool UseHeadCellColor { get; set; }
        IList<TableHeadCell> HeadCells { get; set; }
    }
    public class TableHeadCell
    {
        public string Head { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.None;
    }
}
