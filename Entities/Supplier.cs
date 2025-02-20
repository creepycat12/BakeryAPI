using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }

        public IList<SupplierIngredient> SupplierIngredients { get; set; }
        public SupplierContact SupplierContact { get; set; }
        public IList<SupplierAddress> Addresses { get; set; }
        
        
    }
}