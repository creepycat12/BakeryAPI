using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels.Supplier;

namespace bakery.api.Interfaces;

public interface ISupplierRepository
{
    public Task<IList<SupplierBaseViewModel>> List();
    public Task<SupplierViewModel> Find(string name);
    
}
