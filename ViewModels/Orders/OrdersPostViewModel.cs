using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class OrdersPostViewModel:CustomerPostViewModel
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public IList<ProductsGetViewModel> Products { get; set; }

}
