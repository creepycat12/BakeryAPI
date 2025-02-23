using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class OrdersPostViewModel:CustomerPostViewModel
{
    [Required]
    public string OrderNumber { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public IList<ProductsGetViewModel> Products { get; set; }

}