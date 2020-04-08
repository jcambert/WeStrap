using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeChart
{
    public class DatasetBase
    {
        public string Label { get; internal set; }
        public List<double> Data { get; internal set; } = new List<double>();
        public string Type { get; internal set; }
    }


    public class Dataset : DatasetBase
    {
        public Dataset()
        {
            Console.WriteLine($"Create new Dataset {Id}");

        }

        [JsonIgnore]
        public string Id { get; } = Guid.NewGuid().ToString().Replace("-", "");

        public List<string> BackgroundColor { get; internal set; } = new List<string>();

        public List<string> BorderColor { get; internal set; } = new List<string>();
        public int? BorderWidth { get; internal set; }

        public string BorderCapStyle { get; internal set; }

        public List<int> BorderDash { get; internal set; } = new List<int>();
        public int? BorderDashOffset { get; internal set; }
        public string BorderJoinStyle { get; internal set; }
        public Clip? Clip { get; internal set; }
        public string Fill { get; internal set; }
        public string HoverBackgroundColor { get; internal set; }
        public string HoverBorderCapStyle { get; internal set; }
        public string HoverBorderColor { get; internal set; }
        public List<int> HoverBorderDash { get; internal set; }
        public double? HoverBorderDashOffset { get; internal set; }
        public string HoverBorderJoinStyle { get; internal set; }
        public double? HoverBorderWidth { get; internal set; }
        public double? LineTension { get; internal set; }
        public double? Order { get; internal set; }
        public RGBA? PointBackgroundColor { get; internal set; }
        public RGBA? PointBorderColor { get; internal set; }
        public double? PointBorderWidth { get; internal set; }
        public double? PointHitRadius { get; internal set; }
        public RGBA? PointHoverBackgroundColor { get; internal set; }
        public RGBA? PointHoverBorderColor { get; internal set; }
        public double? PointHoverBorderWidth { get; internal set; }
        public double? PointHoverRadius { get; internal set; }
        public double? PointRadius { get; internal set; }
        public double? PointRotation { get; internal set; }
        public string PointStyle { get; internal set; }

    }
}
