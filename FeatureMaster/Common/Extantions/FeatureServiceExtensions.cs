using FeatureMaster.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureMaster.Common.Extantions;

public static class FeatureServiceExtensions
{
    public static IServiceCollection AddFeatureMaster(this IServiceCollection services, string conectionURL)
    {
        _ = services.AddSingleton<IFeatures, Features>(provider => new Features(conectionURL));
        return services;
    }

    public static IServiceCollection AddFeatureMasterConfig(this IServiceCollection services, Action<FeatConf> configController)
    {
        //some features configurations
        var config = new FeatConf();
        configController(config);

        //do somethings..

        throw new NotImplementedException();
    }
}
