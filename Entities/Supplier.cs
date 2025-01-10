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

        public IList<SupplierProduct> SupplierProducts { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public SupplierAdress SupplierAdress { get; set; }
        
        
    }
}