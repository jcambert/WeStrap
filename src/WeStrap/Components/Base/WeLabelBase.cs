using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WeCommon;

namespace WeStrap
{
    public abstract class WeLabelBase : WeColBase
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("form-check-label", IsCheck)
                .Add("col-form-label", Spacers.Any());
        }

        [CascadingParameter] public IWeEditContext Context { get; set; }

        [Parameter] public bool IsCheck { get; set; }
        [Parameter] public string Label { get; set; }

        [Parameter] public Expression<Func<string>> For { get; set; }

        [Inject] public IStringLocalizerFactory Localizer { get; set; }

        protected string for_input;


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Label = Label ?? GetDisplayName();
            for_input = GetForinput();
        }

        private string GetForinput()
        {

            var expression = (MemberExpression)For.Body;
            return expression.Member.Name;

        }

        private string GetDisplayName()
        {
            string result = null;
            var expression = (MemberExpression)For.Body;

            var value = expression.Member.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            if (value != null)
                //{
                try
                {
                    var _localizer = Localizer.Create(Context.GetModelType());

                    result = _localizer?.GetString(value.Name)?.Value;

                }
                catch (Exception)
                {
                    // Debugger.Break();
                    // Console.WriteLine(ex.Message);
                }
            //}
            return result ?? value?.Name ?? expression.Member.Name ?? "";
        }



    }
}
