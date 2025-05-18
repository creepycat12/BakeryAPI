using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }
    public IList<CustomerOrder> CustomerOrder { get; set; }
    public IList<OrderProduct> OrderProducts { get; set; }


}
