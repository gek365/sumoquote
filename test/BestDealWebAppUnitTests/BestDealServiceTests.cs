using System;
using System.Collections.Generic;
using BestDealWebApp.Models;
using BestDealWebApp.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Moq;

namespace BestDealWebAppUnitTests
{
    public class Tests
    {
        private IBestDealService _bestDealService;

        [SetUp]
        public void Setup()
        {
            var api1Service = new Mock<IQuoteService>();

            api1Service.Setup(mock => mock.GetQuoteAsync(It.IsAny<QuoteRequest>())).ReturnsAsync(1);

            var api2Service = new Mock<IQuoteService>();

            api2Service.Setup(mock => mock.GetQuoteAsync(It.IsAny<QuoteRequest>())).ReturnsAsync(2);

            var api3Service = new Mock<IQuoteService>();

            api3Service.Setup(mock => mock.GetQuoteAsync(It.IsAny<QuoteRequest>())).ReturnsAsync(3);

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider.Setup(mock => mock.GetServices<IQuoteService>()).Returns(new List<IQuoteService>()
                {api1Service.Object, api2Service.Object, api3Service.Object});

            _bestDealService = new BestDealService(serviceProvider.Object);
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, _bestDealService.GetBestDeal(new QuoteRequest()));
        }
    }
}