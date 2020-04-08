using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using WeC;

namespace WeChart
{
    public abstract class WeSerieBase : ComponentBase
    {
        [Parameter] public string Label { get; set; }
        [Parameter] public List<double> Values { get; set; }

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

        [Parameter] public List<WeC.WeColor> Backgrounds { get; set; }

        [Parameter] public HoverStyle? HoverStyle { get; set; }

        [Parameter] public double? Order { get; set; }
        [Parameter] public PointStyle? PointStyle { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        internal string Id = Guid.NewGuid().ToString().Replace("-", "");
        [CascadingParameter] public ChartBase OwnerChart { get; set; }
    }
    public partial class WeSerie<TDataset> : WeSerieBase
        where TDataset : Dataset
    {
        public WeSerie()
        {
        }


        protected override void OnParametersSet()
        {
            OwnerChart.Registry.AddSerie(OwnerChart.Id, this);
        }
    }
}
