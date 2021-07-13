using BestDealWebApp.Models;

namespace BestDealWebApp.Services.Service3
{
    public class Api3Request
    {
        public Address Source { get; set; }

        public Address Destination { get; set; }

        public decimal Packages { get; set; }
    }
}
