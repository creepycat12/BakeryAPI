namespace bakery.api.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string ItemNumber { get; set; }
        public string IngredientName { get; set; }
        public decimal Price_Kg { get; set; }

        public IList<SupplierIngredient> SupplierIngredients { get; set; }
        
        
    }
}