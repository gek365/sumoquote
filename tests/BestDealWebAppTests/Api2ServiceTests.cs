using System;
using System.Net.Http;
using BestDealWebApp.Services.Service2;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace BestDealWebAppTests
{
    [TestFixture]
    public class Api2ServiceTests
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

        [Test(Description = "Service should fail with invalid settings")]
        public void InvalidSettings_AuthValues()
        {
            var options = Options.Create(new Api2ServiceSettings
            {
                UserName = "",
                Password = "test"
            });

            Assert.Throws<ArgumentNullException>(() => new Api2Service(_clientFactory, options),
                $"should throw exception if {nameof(Api2ServiceSettings.UserName)} is invalid");
        }

        [Test(Description = "Service should fail with invalid settings")]
        public void InvalidSettings_Password()
        {
            var options = Options.Create(new Api2ServiceSettings
            {
                UserName = "test",
                Password = ""
            });

            Assert.Throws<ArgumentNullException>(() => new Api2Service(_clientFactory, options),
                $"should throw exception if {nameof(Api2ServiceSettings.Password)} is invalid");
        }

        [Test(Description = "Service should fail with invalid settings")]
        public void InvalidSettings_Uri()
        {
            var options = Options.Create(new Api2ServiceSettings
            {
                UserName = "test",
                Password = "test",
                ApiEndpoint = ""
            });

            Assert.Throws<ArgumentNullException>(() => new Api2Service(_clientFactory, options),
                $"should throw exception if {nameof(Api2ServiceSettings.ApiEndpoint)} is invalid");
        }
        [Test]
        public void ValidSettings()
        {
            var options = Options.Create(new Api2ServiceSettings
            {
                UserName = "test",
                Password = "test",
                ApiEndpoint = "https://google.com",
            });

            Assert.DoesNotThrow(() => new Api2Service(_clientFactory, options),
                "should not throw exception for valid settings");

        }
    }
}