using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class GetProductViewModel: ProductsViewModel
{
    public IList<BatchViewModel> Batches { get; set; }

}
