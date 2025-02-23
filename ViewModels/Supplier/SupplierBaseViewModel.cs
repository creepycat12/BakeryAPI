using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Entities;

namespace bakery.api.ViewModels.Supplier;

public class SupplierBaseViewModel
{
  public string SupplierName { get; set; }
  public IList<IngredientsBaseViewModel> Ingredients { get; set; }
}

