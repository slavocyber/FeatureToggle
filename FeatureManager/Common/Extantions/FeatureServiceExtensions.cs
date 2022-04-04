using FeatureManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureManager.Common.Extantions;

public static class FeatureServiceExtensions
{
    public static IServiceCollection AddFeatureManager(this IServiceCollection services, string configURL)
    {
        _ = services.AddSingleton<IFeatures, Features>(provider => new Features(configURL, new HttpMaster()))
            .AddFeatureManagerConfig(cofig => { });

        return services;
    }

    public static IServiceCollection AddFeatureManagerConfig(this IServiceCollection services, Action<FeatConf> configController)
    {
        //some features configurations
        var config = new FeatConf();
        configController(config);

        //do somethings..

        return services;
    }
}
