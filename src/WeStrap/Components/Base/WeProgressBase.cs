using Microsoft.AspNetCore.Components;
using System;
using WeCommon;
namespace WeStrap
{
    public abstract class WeProgressBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName("progress-bar")
            .Add("progress-bar-striped", IsStriped)
            .Add("progress-bar-animated", IsAnimated)
            .Add($"bg-{Color.ToDescriptionString()}", Color != Color.None);
        }


        protected string ClassnameMulti =>
            new WeStringBuilder().Add("progress").Add(Class).ToString();

        [Parameter] public int Value { get; set; }
        [Parameter] public int Max { get; set; } = 100;
        protected string styles
        {
            get
            {
                if (Value == 0) { return null; }
                var percent = Math.Round((Value / (double)Max) * 100);
                return $"width: {percent}%; {Style}".Trim();
            }
        }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsMulti { get; set; }
        [Parameter] public bool IsBar { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public bool IsAnimated { get; set; }

    }
}
