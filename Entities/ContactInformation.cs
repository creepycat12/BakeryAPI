namespace bakery.api.Entities
{
    public class ContactInformation
    {
        public int SupplierId { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Supplier Supplier { get; set; }
        
        
    }
}