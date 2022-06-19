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
using FeatureManager.Common.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace TestFeatureServer;

[TestClass]
public class TestFeatureManager : IDisposable
{
    private const string GetJsonUrl = "https://localhost:7246/get";
    private const string BaseUrl = "https://localhost:7246/*";
    
    private readonly List<FeatureItem> _features;
    private readonly IFeatManager _featManager;
    private readonly HttpClient _httpClientMock;

    public TestFeatureManager()
    {
        _features = new List<FeatureItem>()
        {
            new() { Name = "new22", Status = true},
            new() { Name = "new2", Status = false}
        };

        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();
        var loggerBw = factory!.CreateLogger<BackgroundWorker>();

        var mockHttp = new MockHttpMessageHandler();
        _ = mockHttp.When(BaseUrl)
                .Respond("features/json", JsonSerializer.Serialize(_features));

        var seting = new SettingsUpdate()
        {
            UrlUpdate = GetJsonUrl,
            IntervalUpdate = 200
        };

        var mockOptions = new Mock<IOptionsSnapshot<SettingsUpdate>>();
        mockOptions.Setup(m => m.Value)
            .Returns(seting);

        _httpClientMock = new HttpClient(mockHttp);

        //var worker = new BackgroundWorker(loggerBw, mockOptions.Object);
        //_featManager = new FeatManager(worker);
        
        Task.Delay(5_000).Wait();
    }

    [TestMethod]
    public void TestBackgroundWorkerInvalidData()
    {
        //Arrange

        //Act

        //Assert
        _ = Assert.ThrowsException<InvalidDataException>(() => _featManager.IsEnable(null!));
        _ = Assert.ThrowsException<InvalidDataException>(() => _featManager.IsEnable(string.Empty));
        Assert.IsFalse(_featManager.IsEnable("1234567890-="));
        Assert.IsFalse(_featManager.IsEnable("new"));
    }

    [TestMethod]
    public void TestBackgroundWorkerValidData()
    {
        //Arrange

        //Act

        //Assert
        Assert.IsTrue(_featManager.IsEnable(_features[0].Name!));
        Assert.IsFalse(_featManager.IsEnable(_features[1].Name!));
    }

    public void Dispose()
    {
        _httpClientMock.Dispose();
    }
}
