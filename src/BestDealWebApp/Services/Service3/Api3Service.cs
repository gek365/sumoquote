using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BestDealWebApp.Services.Service3
{
    public class Api3Service : QuoteServiceBase
    {
        private readonly IXmlMapper<Api3Request, Api3Response> _xmlMapperService;
        private readonly HttpClient _httpClient;

        public Api3Service(
            IHttpClientFactory httpClientFactory,
            IXmlMapper<Api3Request, Api3Response> xmlMapperService,
            IOptions<Api3ServiceSettings> serviceOptions) : base(serviceOptions.Value)
        {
            // (GK) Validate required settings for this service so it fails at startup instead of on call
            if (string.IsNullOrWhiteSpace(UserName))
                throw new ArgumentNullException(nameof(UserName), $"check configuration for {GetType().Name}");

            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentNullException(nameof(Password), $"check configuration for {GetType().Name}");
            _xmlMapperService = xmlMapperService;

            var httpClient = httpClientFactory.CreateClient(GetType().Name);

            httpClient.BaseAddress = ApiEndpointUri;

            var authToken = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(authToken));

            _httpClient = httpClient;
        }
        public override async Task<decimal> GetQuoteAsync(QuoteRequest quoteRequest)
        {
            var api3Request = new Api3Request
            {
                Source = quoteRequest.Source,
                Destination = quoteRequest.Destination,
                // REMARK: (GK) could just set it to 1
                Packages = quoteRequest.Dimensions.Count()
            };

            var response = await _httpClient.PostAsync(
                "quote",
                new StringContent(
                    _xmlMapperService.ToString(api3Request),
                    Encoding.UTF8,
                    "application/xml"));
            
            // TODO: (GK) add proper error response handling
            response.EnsureSuccessStatusCode();

            // TODO: (GK) add error handling
            var api3Response = _xmlMapperService.FromStream(await response.Content.ReadAsStreamAsync());
            
            return api3Response.Quote;
        }
    }
}