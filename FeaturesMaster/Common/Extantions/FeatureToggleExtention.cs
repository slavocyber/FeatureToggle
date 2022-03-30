using FeaturesMaster.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesMaster.Common.Extantions;

public static class FeatureServiceExtensions
{
    public static IServiceCollection AddFeaturesMaster(this IServiceCollection services, string conectionURL)
    {
        _ = services.AddSingleton<IFeatures, Features>(provider => new Features(conectionURL));
        return services;
    }

    public static IServiceCollection AddFeatureConfiguration(this IServiceCollection services, Action<FeatConf> configController)
    {
        //some features configurations
        var config = new FeatConf();
        configController(config);

        //do somethings

        throw new NotImplementedException();
    }
}
