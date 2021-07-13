using System;
using System.Threading.Tasks;

namespace BestDealWebApp.Services
{
    public abstract class QuoteServiceBase : IQuoteService
    {
        protected string ApiKey { get; set; }

        protected string UserName { get; set; }

        protected string Password { get; set; }
        
        protected Uri ApiEndpointUri { get; set; }


        protected QuoteServiceBase(SettingsBase serviceOptionsValue)
        {
            ApiKey = serviceOptionsValue.ApiKey;
            UserName = serviceOptionsValue.UserName;
            Password = serviceOptionsValue.Password;
            var apiEndpointUri = serviceOptionsValue.ApiEndpoint;

            // (GK) ServiceUrl is required for all services, fail early if not set up 
            if (string.IsNullOrWhiteSpace(apiEndpointUri))
                throw new ArgumentNullException(nameof(apiEndpointUri), $"check configuration for {GetType().Name}");

            if (!Uri.TryCreate(apiEndpointUri, UriKind.Absolute, out var endpointUri))
                throw new ArgumentException(nameof(apiEndpointUri), $"invalid url for service: {GetType().Name}");

            ApiEndpointUri = endpointUri;
        }

        public abstract Task<decimal> GetQuoteAsync(QuoteRequest quoteRequest);
    }
}
