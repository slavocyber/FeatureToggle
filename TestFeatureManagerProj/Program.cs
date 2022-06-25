// See https://aka.ms/new-console-template for more information

using FeatureManager.Common.Extantions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestFeatureManagerProj;

await new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddFeatureManager(hostContext.Configuration);
        services.AddSingleton<ITestWork, TestWork>();
    })
    .RunConsoleAsync();



