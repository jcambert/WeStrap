using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WeCommon;
namespace WeStrap
{
    public class WeBadgeBase : WeTag
    {
        public WeBadgeBase()
        {
            Tag = "span";
        }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("badge")
            .Add("badge-pill", IsPill)
            .Add($"badge-{Color.ToDescriptionString()}");
        }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsPill { get; set; }

        private string _href;
        [Parameter]
        public string Href
        {
            get => _href;
            set
            {
                _href = value;
                Tag = value != null || OnClick.HasDelegate ? "a" : "span";
            }
        }
        private EventCallback<MouseEventArgs> _onClick { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick
        {
            get => _onClick;

            set
            {
                _onClick = value;
                Tag = value.HasDelegate || Href != null ? "a" : "span";
            }
        }


        protected void MyOnClick(MouseEventArgs e)
        {
            if (OnClick.HasDelegate) OnClick.InvokeAsync(e);
        }
    }
}
