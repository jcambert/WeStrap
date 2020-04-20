using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeChart;
using WeCommon;
using WeStrap;
using WeStrapApplication.Data;

namespace WeStrapApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });

            services.AddRazorPages()
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(opt =>
                opt.JsonSerializerOptions.IgnoreNullValues = true
                );
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddWeChart<IChartConfiguration, ChartConfiguration>(cfg =>
             {
                 cfg.AddPlugin("waterFallPlugin");

                 cfg.Type = ChartType.Bar.ToDescriptionString();
             },
            cfg =>
            {
                //cfg.AddConverter(new WaterfallDatasetConverter());
            }/*,
            cfg =>
            {
                cfg.UseDatasetCreator<WaterFallDataset>((serie, dataset) =>
               {
                   ((WaterFallDataset)dataset).Stack = "STACK";
                   return dataset;
               });
            }*/);
            // .AddDataset<WaterFallDataset>();
            services.AddWeStrap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
