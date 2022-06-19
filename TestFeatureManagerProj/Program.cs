// See https://aka.ms/new-console-template for more information

using FeatureManager.Common.Extantions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestFeatureManagerProj;

var service = new ServiceCollection();

var dic = new Dictionary<string, string>
{
    { "IntervalUpdate", "2_000"},
    {"UrlUpdate", "https://localhost:7246/get" }
};

IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(dic).Build();

service.AddLogging();
service.AddFeatureManager(config);
service.AddSingleton<ITestWork, TestWork>();

var build = service.BuildServiceProvider();

build.GetRequiredService<ITestWork>().Start();



