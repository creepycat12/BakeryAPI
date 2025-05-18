using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class ProductPostViewModel
{
    public string ProductName { get; set; }
    public decimal PackPrice { get; set; }
    public decimal Weight_kg { get; set; }
    public int AmountInPack { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}
