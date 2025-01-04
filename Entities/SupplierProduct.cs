using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities
{
    public class SupplierProduct
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }

    
    }
}