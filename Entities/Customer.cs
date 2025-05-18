using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl{ get; set; }
    public CustomerContact CustomerContact { get; set; }
     public IList<CustomerAddress> CustomerAddresses { get; set; }
    public IList<CustomerOrder> CustomerOrders { get; set; }
}
