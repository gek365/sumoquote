using System.Collections.Generic;

namespace Api2.Models
{
    public class Request
    {
        public Address Consignee { get; set; }
        
        public Address Consignor { get; set; }

        public IEnumerable<decimal> Cartons { get; set; }
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


