using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Orders;

public class OrderProductViewModel
{
    public string Name { get; set; }
    public decimal PackPrice { get; set; }
    public int AmountInPack { get; set; }
    public decimal Weight_kg { get; set; }
    public decimal PriceEach { get; set; }
    public int Quantity { get; set; }
}
