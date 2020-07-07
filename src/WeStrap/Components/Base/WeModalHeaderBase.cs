using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WeCommon;
namespace WeStrap
{
    public abstract class WeModalHeaderBase : WeTag
    {

        protected string divClassname =>
            new WeStringBuilder().Add("modal-header").ToString();

        protected string headingClassname => 
            new WeStringBuilder()
            .Add("modal-title")
            .Add("container")
            .Add(HeadingClass)
            .ToString();

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public string HeadingClass { get; set; }


    }
}
