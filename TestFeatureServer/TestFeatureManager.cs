using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Feature.Shared;
using FeatureManager.Interfaces;
using FeatureManager.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using FeatureManager;
using System.Threading.Tasks;

namespace TestFeatureServer;

[TestClass]
public class TestFeatureManager : IDisposable
{
    private const string BaseURL = "https://localhost:7246/*";
    private const string GetJsonURL = "https://localhost:7246/get";

    private readonly List<FeatureItem> _features;
    private readonly IFeatureManager _featureManager;
    private readonly HttpClient _httpClientMock;

    public TestFeatureManager()
    {
        _features = new List<FeatureItem>()
        {
            new FeatureItem() { Name = "new22", Status = true},
            new FeatureItem() { Name = "new2", Status = false}
        };

        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();
        //var loggerController = factory!.CreateLogger<JsonMaster>();

        var mockHttp = new MockHttpMessageHandler();
        _ = mockHttp.When(BaseURL)
                .Respond("features/json", JsonSerializer.Serialize(_features));

        _httpClientMock = new HttpClient(mockHttp);

        var worker = new BackgroundWorker(serviceProvider, GetJsonURL, 100);
        _featureManager = new FeatManager(worker);


        Task.Delay(200).Wait();
    }

    [TestMethod]
    public void TestBackgroundWorkerInvalidData()
    {
        //Arrange

        //Act

        //Assert
        _ = Assert.ThrowsException<InvalidDataException>(() => _featureManager.IsEnable(null!));
        _ = Assert.ThrowsException<InvalidDataException>(() => _featureManager.IsEnable(string.Empty));
        Assert.IsFalse(_featureManager.IsEnable("1234567890-="));
        Assert.IsFalse(_featureManager.IsEnable("new"));
    }

    [TestMethod]
    public void TestBackgroundWorkerValidData()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsTrue(_featureManager.IsEnable(_features[0].Name!));
        Assert.IsFalse(_featureManager.IsEnable(_features[1].Name!));
    }

    public void Dispose()
    {
        _httpClientMock.Dispose();
    }
}
