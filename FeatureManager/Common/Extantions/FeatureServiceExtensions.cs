using FeatureManager.Common.Models;
using FeatureManager.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureManager.Common.Extantions;

public static class FeatureServiceExtensions
{
    /// <summary>
    /// Adds FeatureManager to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFeatureManager(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<SettingsUpdate>(_ =>
                config.GetSection(SettingsUpdate.Position))
            .AddHostedService<BackgroundWorker>()
            .AddTransient<IFeatManager, FeatManager>();
    }
}
