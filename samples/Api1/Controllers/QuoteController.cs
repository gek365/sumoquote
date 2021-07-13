using System;
using Api1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;
        
        public QuoteController(ILogger<QuoteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Response Post(Request request)
        {
            var quote = (decimal)new Random().Next(10, 1000) / 100;

            _logger.LogInformation($"{GetType().Namespace} Received request.  Returning Quote {quote}");

            return request != null
                // Remark: returning random number so the service with lowest quote is not always the same
                ? new Response {Total = quote}
                : throw new ArgumentException(nameof(request));
        }
    }
}
