using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeChart
{
    public interface IChartJsRuntime
    {
        Task Create(string chartId, IChartConfiguration configuration,IOptions options=null);

        Task Update(string chartId, IEnumerable<string> labels, string[] data, object options = null);
    }
    internal sealed class ChartJsRuntime : IChartJsRuntime
    {
        private readonly IJSRuntime _js;
        private IJsonConfiguration _jsonConfig;
        public ChartJsRuntime(IJSRuntime jsRuntime, IJsonConfiguration jsonConfig)
        {
            _js = jsRuntime;
            _jsonConfig = jsonConfig;

        }
        private const string JS_HEAD = "WeChart.{0}";


        private string Serialize<T>(T obj) =>
            JsonSerializer.Serialize(obj ?? new Object(), _jsonConfig.SerializeOptions);


        private ValueTask Invoke(string meth, params object[] p)
        {
            return _js.InvokeVoidAsync(string.Format(JS_HEAD, meth), p);
        }
        public async Task Create(string chartId, IChartConfiguration configuration, IOptions options = null)
        {
            var _config = Serialize(configuration);
            var _options = Serialize(options);
            await Invoke("create", chartId,_config, _options);
        }

        public async Task Update(string chartId, IEnumerable<string> labels, string[] data, object options = null)
        {
            //var _data = Serialize(data);
            var _opts = Serialize(options);
            await Invoke("update", chartId, labels ?? new List<string>().ToArray(), data, _opts);
        }



    }
}
