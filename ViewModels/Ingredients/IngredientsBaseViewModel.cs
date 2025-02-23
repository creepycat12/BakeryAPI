using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class IngredientsBaseViewModel
{
    public string Name { get; set; }
    public decimal Price_Kg { get; set; }
    public string ItemNumber { get; set; }
}
