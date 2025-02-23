using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Customers;

public class AddCustomerForRepViewModel
{
    public string Name { get; set; }
    
     public IList<AddressPostViewModel> Addresses { get; set; }
    
    public ContactPostViewModel Contacts { get; set; }
}
