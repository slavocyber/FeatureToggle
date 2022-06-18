// See https://aka.ms/new-console-template for more information

using FeatureManager.Common.Extantions;
using Microsoft.Extensions.DependencyInjection;
using TestFeatureManagerProj;

var service = new ServiceCollection();

service.AddLogging();
service.AddFeatureManager();
service.AddSingleton<TestWork>();

var build = service.BuildServiceProvider();

build.GetRequiredService<TestWork>().Start();



