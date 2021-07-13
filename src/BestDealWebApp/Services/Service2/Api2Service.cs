using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BestDealWebApp.Helpers;
using Microsoft.Extensions.Options;

namespace BestDealWebApp.Services.Service2
{
    public class Api2Service : QuoteServiceBase
    {
        private readonly HttpClient _httpClient;

        public Api2Service(IHttpClientFactory httpClientFactory, IOptions<Api2ServiceSettings> serviceOptions) : base(serviceOptions.Value)
        {
            // (GK) Validate required settings for this service so it fails at startup instead of on call
            if (string.IsNullOrWhiteSpace(UserName))
                throw new ArgumentNullException(nameof(UserName), $"check configuration for {GetType().Name}");

            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentNullException(nameof(Password), $"check configuration for {GetType().Name}");

            var httpClient = httpClientFactory.CreateClient(GetType().Name);

            httpClient.BaseAddress = ApiEndpointUri;

            var authToken = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(authToken));

            _httpClient = httpClient;
        }
        public override async Task<decimal> GetQuoteAsync(QuoteRequest quoteRequest)
        {
            // TODO: (GK) add mapping service when mapping gets complex
            var response = await _httpClient.PostAsync("quote",
                new StringContent(JsonSerializer.Serialize(new
                {
                    Consignee = quoteRequest.Source,
                    Consignor = quoteRequest.Destination,
                    Cartons = quoteRequest.Dimensions
                }), Encoding.UTF8, "application/json"));

            // TODO: (GK) add proper error response handling
            response.EnsureSuccessStatusCode();

            // TODO: (GK) add mapping service when mapping gets complex
            return JsonSerializer.Deserialize<Api2Response>(
                await response.Content.ReadAsStringAsync(), SerializerHelper.SerializerOptions).Amount;
        }
    }
}