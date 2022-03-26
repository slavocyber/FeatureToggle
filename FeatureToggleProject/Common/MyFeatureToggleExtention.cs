using FeaturesMaster;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string conectionURL, Action<FeatConf> configController)
        {
            //some features configurations
            var config = new FeatConf();
            configController(config);

            services.AddSingleton<IFeatures, Features>(provider => new Features(conectionURL));
            return services;
        }
    }
}
