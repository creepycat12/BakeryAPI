namespace bakery.api.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public SupplierContact SupplierContacts { get; set; }
        public CustomerContact CustommerContacts { get; set; }
        
        
    }
}