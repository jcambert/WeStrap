using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WeC;
namespace WeChart
{
    public abstract class DatasetBase
    {
        
        public string Label { get; internal set; }
        public string Type { get; internal set; }
        [JsonIgnore]
        public string Id { get; } = Guid.NewGuid().ToString().Replace("-", "");

        public List<string> BackgroundColor { get; internal set; } = new List<string>();

        public List<string> BorderColor { get; internal set; } = new List<string>();
       

        public string BorderCapStyle { get; internal set; }

        public List<int> BorderDash { get; internal set; } = new List<int>();
        public int? BorderDashOffset { get; internal set; }
        public string BorderJoinStyle { get; internal set; }
        public Clip? Clip { get; internal set; }
        public string Fill { get; internal set; }
        public string HoverBorderCapStyle { get; internal set; }
        
        public List<int> HoverBorderDash { get; internal set; }
        public double? HoverBorderDashOffset { get; internal set; }
        public string HoverBorderJoinStyle { get; internal set; }
        public double? LineTension { get; internal set; }
        public double? Order { get; internal set; }
        public WeColor? PointBackgroundColor { get; internal set; }
        public WeColor? PointBorderColor { get; internal set; }
        public double? PointBorderWidth { get; internal set; }
        public double? PointHitRadius { get; internal set; }
        public WeColor? PointHoverBackgroundColor { get; internal set; }
        public WeColor? PointHoverBorderColor { get; internal set; }
        public double? PointHoverBorderWidth { get; internal set; }
        public double? PointHoverRadius { get; internal set; }
        public double? PointRadius { get; internal set; }
        public double? PointRotation { get; internal set; }
        public string PointStyle { get; internal set; }
    }
    public abstract class DatasetBaseGeneric<TData> : DatasetBase
    {
        public List<TData> Data { get; internal set; } = new List<TData>();

    }


    public class Dataset : DatasetBaseGeneric<double> {
        public int? BorderWidth { get; internal set; }
        public string HoverBackgroundColor { get; internal set; }
        public string HoverBorderColor { get; internal set; }
        public double? HoverBorderWidth { get; internal set; }
    }
    public class DatasetBubble : DatasetBaseGeneric<BubbleData>
    {

        public  List<int> BorderWidth { get; internal set; } = new List<int>();
        public  List<string> HoverBackgroundColor { get; internal set; } = new List<string>();
        public  List<string> HoverBorderColor { get; internal set; } = new List<string>();
        public  List<int> HoverBorderWidth { get; internal set; } = new List<int>();
        public List<int> HoverRadius { get; internal set; } = new List<int>();
        public List<int> HitRadius { get; internal set; } = new List<int>();
        public List<int> Radius { get; internal set; } = new List<int>();

    }
}
