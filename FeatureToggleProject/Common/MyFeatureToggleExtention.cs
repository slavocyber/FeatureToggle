using FeaturesMaster;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string conectionURL)
        {
            services.AddSingleton((Func<IServiceProvider, IFeatures>)(provider => new Features(conectionURL)));
            return services;
        }

        public static IServiceCollection AddFeatureGen(this IServiceCollection services, Action<AddFeatureGenConfiguration>? configure)
        {
            throw new NotImplementedException();
        }
    }
}
