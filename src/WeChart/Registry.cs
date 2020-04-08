﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChart
{

    public interface IRegistry
    {
        void Add(string chartId);
        Task Create(string chartId,ChartBase chart);
        Task Update(string chartId, IEnumerable<string> labels);
        void AddSerie<TDataset>(string chartId, WeSerie<TDataset> serie)
            where TDataset : Dataset;
        string CreateDataset(WeSerieBase value, Type type);
        string[] CreateDataset(string chartId);
    }

    public class Registry : IRegistry
    {
        private readonly IChartJsRuntime js;
        private readonly IChartConfiguration chartConfig;
        private readonly ISerieConfiguration serieConfig;
        private readonly ILogger<Registry> logger;
        private readonly Dictionary<string, List<(WeSerieBase, Type)>> _registry = new Dictionary<string, List<(WeSerieBase, Type)>>();
        public Registry(IChartJsRuntime js, IChartConfiguration chartConfig, ISerieConfiguration serieConfig, ILogger<Registry> logger)
        {
            Console.WriteLine("CREATE REGISTRY");
            this.js = js;
            this.chartConfig = chartConfig;
            this.serieConfig = serieConfig;
            this.logger = logger;
        }

        public void Add(string chartId)
        {
            if (string.IsNullOrEmpty(chartId)) return;
            if (_registry.ContainsKey(chartId))
            {
                logger.LogWarning($"A chart with Id:{chartId} already exists, check your code!!");
                return;
            }
            _registry[chartId] = new List<(WeSerieBase, Type)>();
            logger.LogDebug($"Add Chart:{chartId}");
        }

        /// <summary>
        /// Create a new Chart
        /// </summary>
        /// <param name="chart">the chart to remember</param>
        /// <returns>async</returns>
        public async Task Create(string chartId, ChartBase chart=null)
        {
            if (string.IsNullOrEmpty(chartId)) return;
            if (!_registry.ContainsKey(chartId))
            {
                logger.LogWarning($"A chart with Id:{chartId} already exists, check your code!!");
                return;
            }
            if (!string.IsNullOrEmpty(chart.DefaultChartType))
                chartConfig.Type = chart.DefaultChartType;
            await js.Create(chartId, chartConfig,chart.Options);

            logger.LogDebug($"Create Chart:{chartId}");
        }
        /// <summary>
        /// Update the chart with datasets
        /// </summary>
        /// <param name="chart">The chart to trace</param>
        /// <param name="labels">The labels of this chart</param>
        /// <returns></returns>
        public async Task Update(string chartId, IEnumerable<string> labels)
        {
            if (string.IsNullOrEmpty(chartId)) return;
            logger.LogDebug($"Update Chart {chartId}");
            await js.Update(chartId, labels, CreateDataset(chartId));
        }

        /// <summary>
        /// Add A new Serie to chart
        /// </summary>
        /// <typeparam name="TDataset">Dataset or derived based</typeparam>
        /// <param name="chart">The chart to add serie within</param>
        /// <param name="serie">The serie into this chart</param>
        public void AddSerie<TDataset>(string chartId, WeSerie<TDataset> serie)
            where TDataset : Dataset
        {
            if (string.IsNullOrEmpty(chartId)) return;

            if (!_registry.ContainsKey(chartId))
            {
                logger.LogWarning("You must create Chart and include serie within it");
                return;

            }
            if (_registry[chartId].Where(s => s.Item1.Id == serie.Id).Any())
                return;

            _registry[chartId].Add((serie, typeof(TDataset)));
            logger.LogDebug($"Add Serie {serie.Id} to Chart {chartId}");

        }

        /// <summary>
        /// Generate the dataset to update serie
        /// </summary>
        /// <typeparam name="TDataset"></typeparam>
        /// <param name="serie">The Serie based on</param>
        /// <returns>the json string datatset representation</returns>

        public string CreateDataset(WeSerieBase value, Type type)
        {
            logger.LogDebug($"Create Dataset for type {type.Name}");
            return serieConfig.CreateDataset(value, type);
        }
        /// <summary>
        /// Generate datasets of the chart
        /// </summary>
        /// <param name="chart">the chart based on</param>
        /// <returns>Array of string dataset representation</returns>
        public string[] CreateDataset(string chartId)
        {
            if (string.IsNullOrEmpty(chartId)) return null;
            logger.LogDebug($"Create Dataset for chart {chartId}");

            List<string> result = new List<string>();
            foreach (var item in _registry[chartId])
            {
                result.Add(CreateDataset(item.Item1, item.Item2));
            }
            return result.ToArray();
        }

    }


}
