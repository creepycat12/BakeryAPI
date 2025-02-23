using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels
{
    public class IngredientPostViewModel
    {
        public string ItemNumber { get; set; }
        public string IngredientName { get; set; }
        public decimal Price_Kg { get; set; }
        
    }
}