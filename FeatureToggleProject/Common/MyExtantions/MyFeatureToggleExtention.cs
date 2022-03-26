using Common;
using FeaturesMaster.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesMaster.Common.MyExtantions
{
    public static class FeatureServiceExtensions
    {
        public static IServiceCollection AddFeatureManeger(this IServiceCollection services, string conectionURL)
        {
            services.AddSingleton<IFeatures, Features>(provider => new Features(conectionURL));
            return services;
        }

        public static IServiceCollection AddFeatureFilter<T>(this IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public static IServiceCollection AddFeatureConfiguration(this IServiceCollection services, Action<FeatConf> configController)
        {
            //some features configurations
            var config = new FeatConf();
            configController(config);

            throw new NotImplementedException();
        }

        public static IServiceCollection UseDisabledFeaturesHandler(this IServiceCollection services)
        {
            //services.AddSingleton<IDisabledFeatureHandler, CustomDisabledFeatureHandler>();
            throw new NotImplementedException();
        }
    }
}
