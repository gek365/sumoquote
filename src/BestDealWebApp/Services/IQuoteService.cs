using System.Threading.Tasks;

namespace BestDealWebApp.Services
{
    public interface IQuoteService
    {
        public Task<decimal> GetQuoteAsync(QuoteRequest quoteRequest);
    }
}
