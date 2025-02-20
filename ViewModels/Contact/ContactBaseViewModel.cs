using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels.Contact;

public class ContactBaseViewModel
{
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
