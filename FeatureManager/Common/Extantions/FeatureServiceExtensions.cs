using FeatureManager.Common.Models;
using FeatureManager.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FeatureManager.Common.Extantions;

public static class FeatureServiceExtensions
{
    /// <summary>
    /// Adds FeatureManager to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFeatureManager(this IServiceCollection services)
    {
        /*if (configurationSection is null)
        {
            configurationSection = a =>
            {
                a.IntervalUpdate = 1_000;
                a.UrlUpdate = string.Empty;
            };
        }*/
        
        return services
            //.Configure<SettingsUpdate>(configuration)
            //.AddHostedService<Common.BackgroundWorker>()
            .AddTransient<IHostedService, BackgroundWorker>()
            .AddSingleton<IFeatManager, FeatManager>();
    }
}
