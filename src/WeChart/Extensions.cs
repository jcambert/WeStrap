using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace WeChart
{
    public static class Extensions
    {
        /// <summary>
        /// Add Blazored Chart Support
        /// </summary>
        /// <param name="services"></param>
        /// <param name="chartConfigure"></param>
        /// <param name="jsonConfigure"></param>
        /// <param name="serieConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddWeChart(this IServiceCollection services, Action<IChartConfiguration> chartConfigure = null, Action<IJsonConfiguration> jsonConfigure = null/*, Action<ISerieConfiguration> serieConfigure = null*/)
        {

            return services
                .AddWeChart<IChartConfiguration, ChartConfiguration>(chartConfigure, jsonConfigure/*, serieConfigure*/);
            ;
        }
        /// <summary>
        /// Add Blazored Chart Support
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="chartConfigure"></param>
        /// <param name="jsonConfigure"></param>
        /// <param name="serieConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddWeChart<I, T>(this IServiceCollection services, Action<I> chartConfigure = null, Action<IJsonConfiguration> jsonConfigure = null/*, Action<ISerieConfiguration> serieConfigure = null*/)
            where I : class, IChartConfiguration
            where T : ChartConfigurationBase, I

        {
            services.AddScoped<I, T>(services =>
             {
                 var logger = services.GetRequiredService<ILogger<T>>();
                 var instance = Activator.CreateInstance(typeof(T), logger) as T;
                 chartConfigure?.Invoke(instance);
                 return instance;
             });

            services.AddScoped<IChartJsRuntime, ChartJsRuntime>();

            services.AddScoped<IJsonConfiguration, JsonConfiguration>(services =>
             {
                 var instance = new JsonConfiguration();
                 jsonConfigure?.Invoke(instance);
                 return instance;
             });

            /*services.AddScoped<ISerieConfiguration, SerieConfiguration>(services =>
            {
                var jsonConf = services.GetRequiredService<IJsonConfiguration>();
                var logger = services.GetRequiredService<ILogger<SerieConfiguration>>();
                var mapper = services.GetRequiredService<IMapper>();
                var instance = new SerieConfiguration(jsonConf, logger, mapper);
                serieConfigure?.Invoke(instance);
                return instance;
            });*/
            services.AddScoped<IRegistry, Registry>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(Dataset).Assembly, Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly());
            }, typeof(Dataset).Assembly, Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly());

            return services;
        }

        public static bool ColorBetween(this int value, int from = 0, int to = 255) => value >= from && value <= to;

        public static bool OpacityBetween(this double value, double from = 0.0, double to = 1.0) => value >= from && value <= to;


    }
}
