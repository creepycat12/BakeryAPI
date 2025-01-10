using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string StreetNr { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        
    }
}