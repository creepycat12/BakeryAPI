using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Entities;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Customers;

namespace bakery.api.ViewModels;

public class CustomerPostViewModel
{

    [Required]
    public string Name { get; set; }
    [Required]
    public string DeliveryAddress { get; set; }
    [Required]
    public string DeliveryPostalCode { get; set; }
    public string DeliveryCity { get; set; }
    [Required]
    public string InvoiceAddress { get; set; }
    [Required]
    public string InvoicePostalCode { get; set; }
    public string InvoiceCity { get; set; }
    [Required]
    public string ContactPerson { get; set; }
    [Required]
    public string Phone { get; set; }
    public string Email { get; set; }
    
}
