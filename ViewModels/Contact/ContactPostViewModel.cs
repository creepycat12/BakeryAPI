using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels.Contact;

namespace bakery.api.ViewModels;

public class ContactPostViewModel : ContactBaseViewModel
{
    public int CustomerId { get; set; }
    public int ContactId { get; set; }
    
}
