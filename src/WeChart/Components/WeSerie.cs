using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using WeC;

namespace WeChart
{
    public abstract class WeSerieBase : ComponentBase
    {
        [Parameter] public string Label { get; set; }

        private string _chartType;
        [Parameter]
        public string Type
        {
            get => _chartType;
            set
            {
                if (value != _chartType)
                {
                    _chartType = value;
                }
            }
        }
        [Parameter] public bool Fill { get; set; } = true;

        [Parameter] public LineStyle? LineStyle { get; set; }

        [Parameter] public List<WeColor> Backgrounds { get; set; }

        [Parameter] public HoverStyle? HoverStyle { get; set; }

        [Parameter] public double? Order { get; set; }
        [Parameter] public PointStyle? PointStyle { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        internal string Id = Guid.NewGuid().ToString().Replace("-", "");
        [CascadingParameter] public ChartBase OwnerChart { get; set; }
    }
    public abstract class WeSerieBaseGeneric<TData> : WeSerieBase
    {
        [Parameter] public List<TData> Values { get; set; }

        protected override void OnParametersSet()
        {
            OwnerChart.Registry.AddSerie(OwnerChart.Id, this);
        }

    }
    public partial class WeSerie : WeSerieBaseGeneric<double>{}
    public partial class WeSerieBubble : WeSerieBaseGeneric<BubbleData> {

        [Parameter] public List<WeColor> BorderColor { get; set; }
        [Parameter] public List<int> BorderWidth { get; set; }
        [Parameter] public List<WeColor> HoverBackgroundColor { get; set; }
        [Parameter] public List<WeColor> HoverBorderColor { get; set; }
        [Parameter] public List<int> HoverBorderWidth { get; set; }
        [Parameter] public List<int> HoverRadius { get; set; }
        [Parameter] public List<int> HitRadius { get; set; }
        [Parameter] public List<int> Radius { get; set; }

        protected override void OnParametersSet()
        {
            this.Type = "bubble";
            base.OnParametersSet();
        }
    }

}
