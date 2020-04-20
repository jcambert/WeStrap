using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using WeCommon;
using WeC;
namespace WeChart
{
    /*public interface ISerieConfiguration
    {
        void UseDatasetCreator<TData,TDataset>(Func<WeSerie<TData, TDataset>, TDataset> datasetCreator)
            where TDataset : Dataset;
        void UseDatasetCreator<TData,TDataset>(Func<WeSerie<TData, TDataset>, TDataset, TDataset> datasetCreator)
            where TDataset : Dataset;
        Func<WeSerie<TData, TDataset>, Dataset> GetDatasetCreator<TData,TDataset>()
            where TDataset : Dataset;

        TDataset CreateDataset<TData,TDataset>(WeSerie<TData, TDataset> serie)
            where TDataset : Dataset;
        string CreateDataset(WeSerieBase value, Type type);


    }*/
   /* public class SerieConfiguration : ISerieConfiguration
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
            UseDatasetCreator<double,Dataset>(serie =>
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
                    serie.Backgrounds.ForEach(color => ds.BackgroundColor.Add(color.ToRGBA()));
                else if (serie.LineStyle?.Background != null)
                    serie.Values.ForEach(v => ds.BackgroundColor.Add(serie.LineStyle?.Background?.ToRGBA()));
                if (serie.LineStyle?.BorderStyle?.Color != null)
                    serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.BorderStyle?.Color?.ToRGBA()));
                if (serie.LineStyle?.BorderStyle?.Width > 0 && serie.LineStyle?.Background != null)
                    serie.Values.ForEach(v => ds.BorderColor.Add(serie.LineStyle?.Background.Filled().ToRGBA()));
                return ds;
            });
        }
        public void UseDatasetCreator<TData,TDataset>(Func<WeSerie<TData,TDataset>, TDataset> datasetCreator)
            where TDataset : Dataset
        {
            creators[typeof(TDataset)] = (Func<WeSerieBase, Dataset>)datasetCreator;
        }

        public void UseDatasetCreator<TData,TDataset>(Func<WeSerie<TData, TDataset>, TDataset, TDataset> datasetCreator)
            where TDataset : Dataset
        {
            subcreators[typeof(TDataset)] = (Func<WeSerieBase, Dataset, Dataset>)datasetCreator ;
        }
        public Func<WeSerie<TData, TDataset>, Dataset> GetDatasetCreator<TData,TDataset>()
            where TDataset : Dataset
            =>
            creators.ContainsKey(typeof(TDataset)) ? creators[typeof(TDataset)] : null;



        public TDataset CreateDataset<TData,TDataset>(WeSerie<TData, TDataset> serie)
            where TDataset : Dataset
        {
            
            if (!creators.ContainsKey(typeof(TDataset))) return null;
            var c = creators[typeof(TDataset)];
            var ds = c(serie);
            return ds as TDataset;
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
        }

        
    }*/
}
