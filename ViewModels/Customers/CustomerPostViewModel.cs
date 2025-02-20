using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Entities;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Customers;

namespace bakery.api.ViewModels;

public class CustomerPostViewModel
{

    public string Name { get; set; }
    public string DeliveryAddress { get; set; }
    public string DeliveryPostalCode { get; set; }
    public string DeliveryCity { get; set; }
    public string InvoiceAddress { get; set; }
    public string InvoicePostalCode { get; set; }
    public string InvoiceCity { get; set; }
    public string ContactPerson { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    
}
