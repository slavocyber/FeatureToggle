namespace FeatureToggle.Common
{
    public static class FeatureGenServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureGen(this IServiceCollection services, string conectionURL)
        {
            throw new NotImplementedException();
        }

        public static void ConfigureFeatureGen(this IServiceCollection services, Action<FeatureToggle> setupAction)
        {
            services.Configure(setupAction);
        }

    }
}
