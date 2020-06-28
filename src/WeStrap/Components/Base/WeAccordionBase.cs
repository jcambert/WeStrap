using System;
using System.Threading.Tasks;

namespace WeStrap
{
    public abstract class WeAccordionBase:WeTag
    {
        protected int _currentIndex = -1;
        public abstract bool IsAccordion { get; }
        public string id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");

        public abstract Task Activate(int index);
    }
}
