using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WeChart
{
    public interface IChartConfigurationBase
    {
        void AddPlugin(string name);
    }
    public interface IChartConfiguration : IChartConfigurationBase
    {
        string Type { get; set; }
        public List<string> Plugins { get; set; }
        public IOptions Options { get; set; }

    }

    public abstract class ChartConfigurationBase : IChartConfigurationBase
    {

        public List<string> Plugins { get; set; } = new List<string>();
        public ChartConfigurationBase(ILogger<ChartConfigurationBase> logger)
        {

        }
        public void AddPlugin(string name)
        {
            Plugins.Add(name);
        }
    }
    public class ChartConfiguration : ChartConfigurationBase, IChartConfiguration
    {
        public ChartConfiguration(ILogger<ChartConfiguration> logger) : base(logger)
        {

        }
        public string Type { get; set; }

        public IOptions Options { get; set; }

    }
}
