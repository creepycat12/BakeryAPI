using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using bakery.api.ViewModels.Contact;

namespace bakery.api.ViewModels;

public class ContactPostViewModel : ContactBaseViewModel
{
    [JsonIgnore]
    public int CustomerId { get; set; } 
    [JsonIgnore]
    public int ContactId { get; set; }
    
}
