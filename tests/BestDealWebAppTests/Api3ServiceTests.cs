using System;
using System.Net.Http;
using BestDealWebApp.Services.Service3;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace BestDealWebAppTests
{
    [TestFixture]
    public class Api3ServiceTests
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
        public void InvalidSettings_Username()
        {
            var options = Options.Create(new Api3ServiceSettings
            {
                UserName = "",
                Password = "test"
            });

            Assert.Throws<ArgumentNullException>(() => new Api3Service(_clientFactory, null, options),
                $"should throw exception if {nameof(Api3ServiceSettings.UserName)} is invalid");
        }

        [Test(Description = "Service should fail with invalid settings")]
        public void InvalidSettings_Password()
        {
            var options = Options.Create(new Api3ServiceSettings
            {
                UserName = "test",
                Password = ""
            });

            Assert.Throws<ArgumentNullException>(() => new Api3Service(_clientFactory, null, options),
                $"should throw exception if {nameof(Api3ServiceSettings.Password)} is invalid");
        }

        [Test(Description = "Service should fail with invalid settings")]
        public void InvalidSettings_Uri()
        {
            var options = Options.Create(new Api3ServiceSettings
            {
                UserName = "test",
                Password = "test",
                ApiEndpoint = ""
            });

            Assert.Throws<ArgumentNullException>(() => new Api3Service(_clientFactory, null, options),
                $"should throw exception if {nameof(Api3ServiceSettings.ApiEndpoint)} is invalid");
        }

        [Test]
        public void ValidSettings()
        {
            var options = Options.Create(new Api3ServiceSettings
            {
                UserName = "test",
                Password = "test",
                ApiEndpoint = "https://google.com",
            });

            Assert.DoesNotThrow(() => new Api3Service(_clientFactory, null, options),
                "should not throw exception for valid settings");

        }
    }
}