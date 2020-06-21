namespace WeStrap
{
    public abstract class WeCollapseItemBase : WeTag
    {
        internal WeCollapseBase Collapse { get; set; }

        private bool _active = false;
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (value)
                {
                    Collapse?.Show();
                }
                StateHasChanged();
            }
        }
    }
}
