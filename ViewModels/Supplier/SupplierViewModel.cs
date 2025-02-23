using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels.Address;
using bakery.api.ViewModels.Contact;

namespace bakery.api.ViewModels.Supplier;

public class SupplierViewModel: SupplierBaseViewModel
{
    public AddressViewModel Address  { get; set; }
    public ContactBaseViewModel Contact {get; set;}
    
}
