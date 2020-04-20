using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeC;
using WeCommon;

namespace WeChart
{
    public class ChartProfile : Profile
    {
        public ChartProfile()
        {
            CreateMap<WeSerie, Dataset>()
                .ConstructUsing( (serie,ctx) => {
                   
                    var ds = new Dataset()
                    {
                        Label = serie.Label,
                        Data = serie.Values,
                        Type = serie.Type ?? serie.OwnerChart?.DefaultChartType,
                        BorderCapStyle = serie.LineStyle?.CapStyle.ToDescriptionString(),
                        BorderDash = serie.LineStyle?.BorderStyle?.Dash,
                        BorderDashOffset = serie.LineStyle?.BorderStyle?.DashOffset,
                        BorderJoinStyle = serie.LineStyle?.BorderStyle?.JoinStyle.ToDescriptionString(),
                        Clip = serie.LineStyle?.Clip,
                        Fill = serie.LineStyle?.Fill ?? serie.Fill.ToString().ToLower(),
                        HoverBackgroundColor = serie.HoverStyle?.BackgroundColor.ToString(),
                        HoverBorderCapStyle = serie.HoverStyle?.BorderCapStyle,
                        HoverBorderColor = serie.HoverStyle?.BorderColor.ToString(),
                        HoverBorderDash = serie.HoverStyle?.BorderDash,
                        HoverBorderDashOffset = serie.HoverStyle?.BorderDashOffset,
                        HoverBorderJoinStyle = serie.HoverStyle?.BorderJoinStyle,
                        HoverBorderWidth = serie.HoverStyle?.BorderWidth,
                        LineTension = serie.LineStyle?.Tension,
                        Order = serie.Order,
                        PointBackgroundColor = serie.PointStyle?.BackgroundColor,
                        PointBorderColor = serie.PointStyle?.BorderColor,
                        PointBorderWidth = serie.PointStyle?.BorderWidth,
                        PointHitRadius = serie.PointStyle?.HitRadius,
                        PointHoverBackgroundColor = serie.PointStyle?.HoverBackgroundColor,
                        PointHoverBorderColor = serie.PointStyle?.HoverBorderColor,
                        PointHoverBorderWidth = serie.PointStyle?.HoverBorderWidth,
                        PointHoverRadius = serie.PointStyle?.HoverRadius,
                        PointRadius = serie.PointStyle?.Radius,
                        PointRotation = serie.PointStyle?.Rotation,
                        PointStyle = serie.PointStyle?.Style,

                    };
                    if (serie.Values!=null && serie.Values.Any() )
                    {
                        if (serie.Backgrounds?.Count > 0 || false)
                            serie.Backgrounds.ForEach(color => ds.BackgroundColor.Add(color.ToRGBA()));
                        else if (serie.LineStyle?.Background != null)
                            serie.Values.ForEach(v => ds.BackgroundColor.Add(serie.LineStyle?.Background?.ToRGBA()));
                        if (serie.LineStyle?.BorderStyle?.Color != null)
                            serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.BorderStyle?.Color?.ToRGBA()));
                        if (serie.LineStyle?.BorderStyle?.Width > 0 && serie.LineStyle?.Background != null)
                            serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.Background.Filled().ToRGBA()));
                    }
                    return ds;
                });

            CreateMap<WeSerieBubble, DatasetBubble>()
                .ConstructUsing((serie, ctx) => {

                    var ds = new DatasetBubble()
                    {
                        Label = serie.Label,
                        Data = serie.Values,
                        Type = serie.Type ?? serie.OwnerChart?.DefaultChartType,
                        BorderCapStyle = serie.LineStyle?.CapStyle.ToDescriptionString(),
                        BorderDash = serie.LineStyle?.BorderStyle?.Dash,
                        BorderDashOffset = serie.LineStyle?.BorderStyle?.DashOffset,
                        BorderJoinStyle = serie.LineStyle?.BorderStyle?.JoinStyle.ToDescriptionString(),
                        Clip = serie.LineStyle?.Clip,
                        Fill = serie.LineStyle?.Fill ?? serie.Fill.ToString().ToLower(),
                        //HoverBackgroundColor = serie.HoverStyle?.BackgroundColor.ToString(),
                        HoverBorderCapStyle = serie.HoverStyle?.BorderCapStyle,
                        //HoverBorderColor = serie.HoverStyle?.BorderColor.ToString(),
                        HoverBorderDash = serie.HoverStyle?.BorderDash,
                        HoverBorderDashOffset = serie.HoverStyle?.BorderDashOffset,
                        HoverBorderJoinStyle = serie.HoverStyle?.BorderJoinStyle,
                        //HoverBorderWidth = serie.HoverStyle?.BorderWidth,
                        LineTension = serie.LineStyle?.Tension,
                        Order = serie.Order,
                        PointBackgroundColor = serie.PointStyle?.BackgroundColor,
                        PointBorderColor = serie.PointStyle?.BorderColor,
                        PointBorderWidth = serie.PointStyle?.BorderWidth,
                        PointHitRadius = serie.PointStyle?.HitRadius,
                        PointHoverBackgroundColor = serie.PointStyle?.HoverBackgroundColor,
                        PointHoverBorderColor = serie.PointStyle?.HoverBorderColor,
                        PointHoverBorderWidth = serie.PointStyle?.HoverBorderWidth,
                        PointHoverRadius = serie.PointStyle?.HoverRadius,
                        PointRadius = serie.PointStyle?.Radius,
                        PointRotation = serie.PointStyle?.Rotation,
                        PointStyle = serie.PointStyle?.Style,

                    };
                    if (serie.Values != null && serie.Values.Any())
                    {
                        #region BackGroundColor
                        if (serie.Backgrounds?.Count > 0 || false)
                            serie.Backgrounds.ForEach(color => ds.BackgroundColor.Add(color.ToRGBA()));
                        else if (serie.LineStyle?.Background != null)
                            serie.Values.ForEach(v => ds.BackgroundColor.Add(serie.LineStyle?.Background?.ToRGBA()));
                        #endregion
                        #region Bordercolor
                        if (serie.LineStyle?.BorderStyle?.Color != null)
                            serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.BorderStyle?.Color?.ToRGBA()));
                        else if (serie.LineStyle?.BorderStyle?.Width > 0 && serie.LineStyle?.Background != null)
                            serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.Background.Filled().ToRGBA()));
                        else if (serie.BorderColor?.Count > 0 || false)
                            serie.BorderColor.ForEach(color => ds.BorderColor.Add(color.ToRGBA()));
                        #endregion
                        if (serie.BorderWidth?.Count > 0 || false)
                            serie.BorderWidth.ForEach(value => ds.BorderWidth.Add(value));
                        if (serie.HoverBackgroundColor?.Count > 0 || false)
                            serie.HoverBackgroundColor.ForEach(value => ds.HoverBackgroundColor.Add(value.ToRGBA()));
                        if (serie.HoverBorderColor?.Count > 0 || false)
                            serie.HoverBorderColor.ForEach(value => ds.HoverBorderColor.Add(value.ToRGBA()));
                        if (serie.HoverBorderWidth?.Count > 0 || false)
                            serie.HoverBorderWidth.ForEach(value => ds.HoverBorderWidth.Add(value));
                        if (serie.HoverRadius?.Count > 0 || false)
                            serie.HoverRadius.ForEach(value => ds.HoverRadius.Add(value));
                        if (serie.HitRadius?.Count > 0 || false)
                            serie.HitRadius.ForEach(value => ds.HitRadius.Add(value));
                        if (serie.Radius?.Count > 0 || false)
                            serie.Radius.ForEach(value => ds.Radius.Add(value));
                    }
                    return ds;
                });

        }

        /*T To<T>(params T[] values) =>
            values.ToList().Where(p => p != null).First();*/
    }
}
