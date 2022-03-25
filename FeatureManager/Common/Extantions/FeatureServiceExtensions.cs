using FeatureManager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureManager.Common.Extantions;

public static class FeatureServiceExtensions
{
    /// <summary>
    /// Adds FeatureManager to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionUrl">way or url for json file</param>
    /// <returns></returns>
    public static IServiceCollection AddFeatureManager(this IServiceCollection services, string connectionUrl, int intervalTime)
    {
        _ = services
            .AddSingleton<IFeatureManager, FeatManager>()
            .AddSingleton<IBackgroundWorker, BackgroundWorker>(bw => new BackgroundWorker(new HttpClient(), connectionUrl, intervalTime));

        return services;
    }
}
