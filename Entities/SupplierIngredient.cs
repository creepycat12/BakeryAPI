using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities
{
    public class SupplierIngredient
    {
        public int IngredientId { get; set; }
        public int SupplierId { get; set; }
        
        public Ingredient ingredient { get; set; }
        public Supplier Supplier { get; set; }

    
    }
}