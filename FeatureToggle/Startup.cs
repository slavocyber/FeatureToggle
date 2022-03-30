using FeaturesMaster.Common.Extantions;

namespace FeatureToggle;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddOptions();
        _ = services.AddControllers();
        _ = services.AddMvc();

        _ = services.AddFeatureMaster("Toggle.json")
            .AddFeatureConfiguration(config =>
            {
                config.SomeConfigOne = false;
                config.SomeConfigTwo = false;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            _ = app.UseDeveloperExceptionPage();
        }

        _ = app.UseRouting();

        _ = app.UseEndpoints(endpoints =>
          {
              _ = endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Main}/{action=Index}");
          });
    }
}
