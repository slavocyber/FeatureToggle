using Interfaces;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using testzxc;

namespace Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string conectionURL)
        {
            services.AddSingleton<IFeatureToggle>(provider => new FeatureToggle(conectionURL));
            return services;
        }

        public static IServiceCollection AddFeatureGen(this IServiceCollection services, Action<MvcOptions>? configure)
        {
            throw new NotImplementedException();
        }
    }


    public class MvcOptions
    { 
        public bool? Enable { get; set; }
        public string ? Description { get; set; }
        public bool? Disable { get; set; }
    }
}
