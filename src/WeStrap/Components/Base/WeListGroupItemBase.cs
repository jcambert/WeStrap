using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeListGroupItemBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("list-group-item")
            .Add($"list-group-item-{Color.ToDescriptionString()}", Color != Color.None)
            .Add("active", IsActive)
            .Add("disabled", IsDisabled && ListGroupType != ListGroupType.Button);
        }

        protected override string Tag => ListGroupType switch
        {
            ListGroupType.Button => "button",
            ListGroupType.Link => "a",
            ListGroupType.List => "li",
            _ => "li"
        };

        protected string href => ListGroupType == ListGroupType.Link ? "javascript:void(0)" : null;
        protected string IsButton => Tag == "button" ? "button" : "";

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public string Href { get; set; }
        [Parameter]public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [CascadingParameter] public WeListGroup Parent { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Parent == null)
                throw new ArgumentException("WeListGroupItem must be a child of WeListGroup");
            ListGroupType = Parent.ListGroupType;
        }
    }
}
