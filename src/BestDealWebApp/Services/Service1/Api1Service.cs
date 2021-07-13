using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BestDealWebApp.Helpers;
using BestDealWebApp.Models;
using Microsoft.Extensions.Options;

namespace BestDealWebApp.Services.Service1
{
    public class Api1Service : QuoteServiceBase
    {
        private readonly HttpClient _httpClient;

        public Api1Service(IHttpClientFactory httpClientFactory, IOptions<Api1ServiceSettings> serviceOptions): base(serviceOptions.Value)
        {
            // (GK) Validate required settings for this service so it fails at startup instead of on call
            if (string.IsNullOrWhiteSpace(ApiKey))
                throw new ArgumentNullException(nameof(ApiKey), $"check configuration for {GetType().Name}");

            var httpClient = httpClientFactory.CreateClient(GetType().Namespace); 

            httpClient.BaseAddress = ApiEndpointUri;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            
            _httpClient = httpClient;
        }
        public override async Task<decimal> GetQuoteAsync(QuoteRequest quoteRequest)
        {
            // TODO: (GK) add mapping service when mapping gets complex
            var response = await _httpClient.PostAsync("quote",
                new StringContent(JsonSerializer.Serialize(new
                {
                    Contact = quoteRequest.Source,
                    Warehouse = quoteRequest.Destination,
                    Package = quoteRequest.Dimensions
                }), Encoding.UTF8, "application/json"));
            
            // TODO: (GK) add proper error response handling
            response.EnsureSuccessStatusCode();

            // TODO: (GK) add mapping service when mapping gets complex
            return JsonSerializer.Deserialize<Api1Response>(
                await response.Content.ReadAsStringAsync(), SerializerHelper.SerializerOptions).Total;
        }
    }
}
