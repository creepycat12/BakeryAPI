using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Orders;

public class OrdersViewModel
{
     public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Customer { get; set; } 
    public List<OrderProductViewModel> Products { get; set; } 
    public decimal Total { get; set; } 
    
}
