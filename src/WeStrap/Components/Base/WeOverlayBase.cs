using Microsoft.AspNetCore.Components;
using WeC;
using WeCommon;

namespace WeStrap
{
    public abstract class WeOverlayBase : WeTag
    {
        protected override WeStringBuilder BuildClassName(string s = "")=>
            base.BuildClassName(s)
            .Add("overlay")
            .Add("bottom",IsBottom)
            .Add(Height.ToDescriptionString())
            ;
        protected override WeStringBuilder BuildStyle()=>
            base.BuildStyle()
            .Add($"opacity:{Opacity};",Opacity>=0.0 && Opacity<=1.0)
            .Add($"background:linear-gradient({Direction}deg, {StartColor.ToRGBA()} {StartColorLength.ToString()}, {EndColor.ToRGBA()} {EndColorLength.ToString()});")
            ;
        
        
        [Parameter] public bool IsBottom { get; set; } = false;
        [Parameter] public Height Height { get; set; }=Height.H200;
        [Parameter] public int Direction { get; set; } = -180;
        [Parameter] public Percentage Opacity { get; set; } = 85;
        [Parameter] public WeColor StartColor { get; set; } = WeC.WeColor.From(0, 0, 0, 0);
        [Parameter] public WeColor EndColor { get; set; } =WeColor.From("#000");
        [Parameter] public Percentage StartColorLength { get; set; } = 0.12;
        [Parameter] public Percentage EndColorLength { get; set; } = 0.97;

    }
}
