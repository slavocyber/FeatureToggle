using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Feature.Server.Common;
using Feature.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFeatureServer;

[TestClass]
public class TestServer
{
    private readonly List<FeatureItem> _features;
    private readonly JsonMaster _jsonMaster;

    public TestServer()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();
        var loggerController = factory!.CreateLogger<JsonMaster>();

        _jsonMaster = new JsonMaster(loggerController);

        _features = new List<FeatureItem>()
        {
            new FeatureItem() { Name = "new22", Status = true},
            new FeatureItem() { Name ="new2", Status = false}
        };
    }

    [TestMethod]
    public void TestWriteReadToJson()
    {
        _ = _jsonMaster.WriteAsync(_features);

        var str = _jsonMaster.ReadAsync().Result;
        var feats = JsonSerializer.Deserialize<List<FeatureItem>>(str);

        foreach (var feature in _features)
        {
            Assert.IsTrue(feats!.Any(f => f.Name!.Equals(feature.Name, StringComparison.Ordinal)
                                               && f.Status == feature.Status));
        }
    }
}
