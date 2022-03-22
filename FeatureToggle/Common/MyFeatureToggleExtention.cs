using FeatureToggle.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string featureURL)
        {
            services.Configure(delegate (MvcOptions c) //???!!!
            {
                //c.Conventions.Add(new SwaggerApplicationConvention());
                
                throw new NotImplementedException();
            });
            services.AddTransient<IFeatureToggle, FeatureToggle>();

            return services;
        }

        public static void ConfigureFeatureGen(this IServiceCollection services, Action<FeatureToggle> setupAction)
        {
            services.Configure(setupAction);
        }

    }
}
