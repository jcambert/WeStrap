using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace WeChart
{
    public enum RgbaType
    {
        [Description("backgroundColor")]
        Background,
        [Description("borderColor")]
        Border
    }
    public class WeRgba<TDataset> : ComponentBase
        where TDataset : Dataset
    {
        /// <summary>
        /// Red Color
        /// </summary>
        [Parameter] public int Red { get; set; }
        /// <summary>
        /// Grenen Color
        /// </summary>
        [Parameter] public int Green { get; set; }
        /// <summary>
        /// Blue Color
        /// </summary>
        [Parameter] public int Blue { get; set; }
        [Parameter] public double Opacity { get; set; } = 0.2;
        [Parameter] public RgbaType Type { get; set; } = RgbaType.Background;
        [Parameter] public bool FillBorder { get; set; } = true;
        [CascadingParameter] public WeSerie<TDataset> Serie { get; set; }

        protected override void OnParametersSet()
        {
            /* this.Serie.Dataset.BackgroundColor.Add(new RGBA(Red, Green, Blue, Opacity).ToString());
             if(FillBorder)
                 this.Serie.Dataset.BorderColor.Add(new RGBA(Red, Green, Blue, 1).ToString());*/
        }
    }
}
