using System;
using Api3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api3.Controllers
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
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public ActionResult<Api3Response> Post(Api3Request request)
        {
            var quote = (decimal)new Random().Next(10, 1000) / 100;

            _logger.LogInformation($"{GetType().Namespace} Received request.  Returning Quote {quote}");

            return request != null
                // Remark: (GK) returning random number so the service with lowest quote is not always the same
                ? new Api3Response {Quote = quote}
                : throw new ArgumentNullException(nameof(request));
        }
    }
}
