using FeaturesMaster;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string conectionURL, Func<FeatConf> configure)
        {
            services.AddSingleton<IFeatures, Features>(provider => new Features(conectionURL, configure.Invoke()));
            return services;
        }
    }
}
