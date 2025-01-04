namespace bakery.api.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ItemNumber { get; set; }
        public string ProductName { get; set; }
        public float Price_Kg { get; set; }

        public IList<SupplierProduct> SupplierProducts { get; set; }
        
        
    }
}