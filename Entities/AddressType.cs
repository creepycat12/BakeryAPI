using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class AddressType
{
    public int Id { get; set; }
    public string Value { get; set; }
    public IList<Address> Addresses { get; set; }

}
