using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WeQ;


namespace WeStrap
{
    public static class Extensions
    {
        public static IServiceCollection AddWeStrap(this IServiceCollection serviceCollection,Action<IWeStrapSpinner> cfgSpinner=null)
        {
            serviceCollection.AddWeQuery();
            serviceCollection.AddSingleton<CurrentTheme>();
            serviceCollection.AddSingleton<IWeStrapSpinner, WeStrapSpinner>(services=>
            {
                var result= new WeStrapSpinner();
                cfgSpinner?.Invoke(result);
                return result;
            });
            serviceCollection.AddTransient<IWeStrapTheme, WeStrapTheme>();
            serviceCollection.AddSingleton<StringService>();
            return serviceCollection;
        }
        public static bool IsFieldRequired<TModel>(this TModel model, string fieldName)
        {
            return typeof(TModel).GetProperty(fieldName)?.GetCustomAttributes(typeof(ValidationAttribute), true).Any() ?? false;
        }
        


        public static bool MatchActiveRoute(this string currentUriAbsolute, string hrefAbsolute)
        {
            hrefAbsolute = hrefAbsolute?.Replace("://", ":///");
            hrefAbsolute = hrefAbsolute?.Replace("//", "/");

            if (hrefAbsolute.ToUpperInvariant() == currentUriAbsolute?.ToUpperInvariant())
            {
                return true;
            }
            if (currentUriAbsolute.Length == hrefAbsolute?.Length - 1)
            {
                if (hrefAbsolute[hrefAbsolute.Length - 1] == '/'
                    && hrefAbsolute.StartsWith(currentUriAbsolute, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool MatchActiveRoute(this Uri currentUriAbsolute, string hrefAbsolute)
        {
            return MatchActiveRoute(currentUriAbsolute?.AbsoluteUri, hrefAbsolute);
        }
    }
}
