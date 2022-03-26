using FeaturesMaster.Common;
using FeaturesMaster.Common.MyExtantions;

namespace FeatureToggle
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddOptions();
            services.AddControllers();
            services.AddMvc();

            services.AddFeatureManeger("Toggle.json",
                config =>
                {
                    config.SomeConfigOne = true;
                    config.SomeConfigTwo = false;
                })
                .UseDisabledFeaturesHandler();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Main}/{action=Index}");
            });
        }
    }
}
