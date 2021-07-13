using System.Collections.Generic;

namespace Api1.Models
{
    public class Request
    {
        public Address ContactAddress { get; set; }
        
        public Address WarehouseAddress { get; set; }

        public IEnumerable<decimal> Dimensions { get; set; }
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


