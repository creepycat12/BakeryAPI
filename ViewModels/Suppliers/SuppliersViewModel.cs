using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Suppliers;

public class SuppliersViewModel
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string AddressLine { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public string Postalcode { get; set; }
    public string Email { get; set; }
}
