using System;
using System.Net.Http;
using BestDealWebApp.Services.Service1;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace BestDealWebAppTests
{
    [TestFixture]
    public class Api1ServiceTests
    {
        private IHttpClientFactory _clientFactory;

        [SetUp]
        public void Setup()
        {
            var mockClientFactory = new Mock<IHttpClientFactory>();

            mockClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            _clientFactory = mockClientFactory.Object;
        }

        [Test(Description = "Service should fail with invalid api-key at init")]
        public void InvalidSettings_ApiKey()
        {
            var options = Options.Create(new Api1ServiceSettings()
            {
                ApiKey = "",
            });

            Assert.Throws<ArgumentNullException>(() => new Api1Service(_clientFactory, options),
                "should throw exception if api keys is invalid");
        }

        [Test(Description = "Service should fail with invalid uri at init")]
        public void InvalidSettings_Uri()
        {
            var options = Options.Create(new Api1ServiceSettings()
            {
                ApiKey = "test",
                ApiEndpoint = ""
            });

            Assert.Throws<ArgumentNullException>(() => new Api1Service(_clientFactory, options),
                $"should throw exception if {nameof(Api1ServiceSettings.ApiEndpoint)} is invalid");
        }

        [Test]
        public void ValidSettings()
        {
            var options = Options.Create(new Api1ServiceSettings
            {
                ApiKey = "test-key",
                ApiEndpoint = "https://google.com",
            });

            Assert.DoesNotThrow(() => new Api1Service(_clientFactory, options),
                "should not throw exception for valid settings");

        }
    }
}