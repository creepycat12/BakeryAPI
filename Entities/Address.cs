using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }

        // [ForeignKey("PostalAddressId")]
        public int AddressTypeId { get; set; }
        public int PostalAddressId { get; set; }

        public AddressType AddressType { get; set; }
        public PostalAddress PostalAddress { get; set; }
        public IList<SupplierAddress> SupplierAddresses { get; set; }
        public IList<CustomerAddress> CustomerAddresses { get; set; }
       
    
        
    }
}