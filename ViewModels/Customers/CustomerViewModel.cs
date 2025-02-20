using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels.Address;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Orders;

namespace bakery.api.ViewModels.Customers;

public class CustomerViewModel:BaseCustomerViewModel
{
    public IList<AddressViewModel> Addresses { get; set; } 
    public IList<OrdersViewModel> Orders { get; set; } 
    public ContactViewModel Contacts { get; set; }
}
