using System.Collections.Generic;
using System.Net.Http;
using Feature.Shared;
using NUnit.Framework;

namespace TestServer
{
    public class TestUpdateControlle
    {
        private const string GetJsonURL = "/get";
        private const string UpdateJsonURL = "/update";

        private readonly HttpClient _httpClient;
        private readonly List<FeatureItem> _features;

        public TestUpdateControlle(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
