using System.Collections.Generic;

namespace BestDealWebApp.Services
{
    public class QuoteRequest
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }

        // Remark: (GK) not sure if dimensions should be a class, leaving as decimal for this.
        public IEnumerable<decimal> Dimensions { get; set; }
    }
}
