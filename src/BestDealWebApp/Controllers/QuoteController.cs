using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BestDealWebApp.Services;
using BestDealWebApp.Services.InputDataService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace BestDealWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly IDataService _dataService;
        private readonly IEnumerable<IQuoteService> _quoteServices;

        public QuoteController(
            IDataService dataService, 
            IServiceProvider serviceProvider,
            ILogger<QuoteController> logger)
        {
            _dataService = dataService;
            _quoteServices = serviceProvider.GetServices<IQuoteService>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var request = _dataService.GetRequest()
                          ?? throw new Exception(
                              $"no data to process, make sure {nameof(IDataService)} is configured");

            var watch = Stopwatch.StartNew();

            // TODO: (GK) add business service for complex best quote selection
            var bestDeal = (await Task.WhenAll(_quoteServices.Select(service =>
                    service.GetQuoteAsync(request)))).Min();
            
            watch.Stop();

            return $"Processed request.  Min amount: {bestDeal}.  Process time: {watch.ElapsedMilliseconds} ms";
        }
    }
}
