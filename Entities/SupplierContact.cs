using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class SupplierContact
{
    public int SupplierId { get; set; }
    public int ContactId { get; set; }
    public Supplier Supplier { get; set; }
    public Contact Contact { get; set; }
}
