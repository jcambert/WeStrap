using System.Collections.Generic;

namespace WeStrap
{
    public class WeMenuItem
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string NavigateTo { get; set; }
        public List<WeMenuItem> Childs { get; set; } = new List<WeMenuItem>();
    }
}
