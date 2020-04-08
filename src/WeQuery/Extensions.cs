using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace WeQ
{
    public static class Extensions
    {
        public static IServiceCollection AddWeQuery(this IServiceCollection services)
        {
            services.AddTransient<IWeQuery, WeQueryJS>();
            return services;
        }

        public static string ToTitleCase(this string s) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
    }
}
