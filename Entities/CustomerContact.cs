using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class CustomerContact
{
    public int CustomerId { get; set; }
    public int ContactId { get; set; }
    public Customer Customer { get; set; }
    public Contact Contact { get; set; }
}
