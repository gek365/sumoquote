using System.Collections.Generic;

namespace Api3.Models
{

    public class Api3Request
    {
        public Address Source { get; set; }
        
        public Address Destination { get; set; }

        public decimal Packages { get; set; }
    }

    // Stubs
    public class Address
    {
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}


