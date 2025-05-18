using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class CustomerAddress
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
    public Customer Customer { get; set; }
    public Address Address { get; set; }

}
