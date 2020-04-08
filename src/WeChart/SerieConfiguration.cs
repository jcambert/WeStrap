using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using WeCommon;

namespace WeChart
{
    public interface ISerieConfiguration
    {
        void UseDatasetCreator<TDataset>(Func<WeSerieBase, Dataset> datasetCreator)
            where TDataset : Dataset;
        void UseDatasetCreator<TDataset>(Func<WeSerieBase, Dataset, Dataset> datasetCreator)
            where TDataset : Dataset;
        Func<WeSerieBase, Dataset> GetDatasetCreator<TDataset>()
            where TDataset : Dataset;

        Dataset CreateDataset<TDataset>(WeSerieBase serie)
            where TDataset : Dataset;
        string CreateDataset(WeSerieBase value, Type type);


    }
    public class SerieConfiguration : ISerieConfiguration
    {
        Dictionary<Type, Func<WeSerieBase, Dataset>> creators = new Dictionary<Type, Func<WeSerieBase, Dataset>>();
        Dictionary<Type, Func<WeSerieBase, Dataset, Dataset>> subcreators = new Dictionary<Type, Func<WeSerieBase, Dataset, Dataset>>();

        private readonly IJsonConfiguration jsonConfig;
        private readonly ILogger<SerieConfiguration> logger;
        private readonly IMapper mapper;
        public SerieConfiguration(IJsonConfiguration jsonConfig, ILogger<SerieConfiguration> logger, IMapper mapper)
        {
            this.jsonConfig = jsonConfig;
            this.logger = logger;
            this.mapper = mapper;
            UseDatasetCreator<Dataset>(serie =>
            {
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
                if(serie.Backgrounds?.Count>0 || false)
                    serie.Backgrounds.ForEach(color => ds.BackgroundColor.Add(color.ToString()));
                else if (serie.LineStyle?.Background != null)
                    serie.Values.ForEach(v => ds.BackgroundColor.Add(serie.LineStyle?.Background.ToString()));
                if (serie.LineStyle?.BorderStyle?.Color != null)
                    serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.BorderStyle?.Color.ToString()));
                if (serie.LineStyle?.BorderStyle?.Width > 0 && serie.LineStyle?.Background != null)
                    serie.Values.ForEach(v => ds.BorderColor.Add(RGBA.Filled(serie.LineStyle?.Background).ToString()));
                return ds;
            });
        }
        public void UseDatasetCreator<TDataset>(Func<WeSerieBase, Dataset> datasetCreator)
            where TDataset : Dataset
        {
            creators[typeof(TDataset)] = datasetCreator;
        }

        public void UseDatasetCreator<TDataset>(Func<WeSerieBase, Dataset, Dataset> datasetCreator)
            where TDataset : Dataset
        {
            subcreators[typeof(TDataset)] = datasetCreator;
        }
        public Func<WeSerieBase, Dataset> GetDatasetCreator<TDataset>()
            where TDataset : Dataset
            =>
            creators.ContainsKey(typeof(TDataset)) ? creators[typeof(TDataset)] : null;



        public Dataset CreateDataset<TDataset>(WeSerieBase serie)
            where TDataset : Dataset
        {
            /*if (!creators.ContainsKey(typeof(TDataset)))
            {
                return JsonSerializer.Serialize(new Object());
            }
            return JsonSerializer.Serialize(serie, typeof(TDataset), jsonConfig.SerializeOptions);*/
            if (!creators.ContainsKey(typeof(TDataset))) return null;
            var c = creators[typeof(TDataset)];
            var ds = c(serie);
            return ds;
        }

        public string CreateDataset(WeSerieBase serie, Type type)
        {
            if (creators.ContainsKey(type))
            {

                var c = creators[type];
                var ds = c(serie);
                return JsonSerializer.Serialize(ds, type, jsonConfig.SerializeOptions);
            }

            else if (subcreators.ContainsKey(type))
            {
                var c = creators[typeof(Dataset)];
                var ds = c(serie);
                var sds = subcreators[type](serie, (Dataset)mapper.Map(ds, typeof(Dataset), type));
                return JsonSerializer.Serialize(sds, type, jsonConfig.SerializeOptions);
            }
            return null;

            //var serieType=Type.MakeGenericSignatureType(typeof(WeSerie<>), type);
            //var del=Convert((Func<object, object>)c , serieType, type);
            //return JsonSerializer.Serialize(ds, type, jsonConfig.SerializeOptions);
        }

        /*public static Delegate Convert(Func<object, object> func, Type argType, Type resultType)
        {


            var param = Expression.Parameter(argType);

            var converted = Expression.Convert(
                Expression.Call(func.Method, Expression.Convert(param, typeof(object))),
                resultType);

            var delegateType = typeof(Func<,>).MakeGenericType(argType, resultType);
            return Expression.Lambda(delegateType, converted, param).Compile();
        }*/
    }
}
