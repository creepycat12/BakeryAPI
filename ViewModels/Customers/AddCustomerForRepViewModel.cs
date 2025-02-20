using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Customers;

public class AddCustomerForRepViewModel:BaseCustomerViewModel
{
     public IList<AddressPostViewModel> Addresses { get; set; }
    
    public ContactPostViewModel Contacts { get; set; }
}
