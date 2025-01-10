using System.Text.Json;
using bakery.api.Entities;
namespace bakery.api.Data
{
    public static class Seed
    {
        public static async Task LoadProducts(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Products.Any()) return;

            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options); 
            
            if(products is not null && products.Count>0){
               await context.Products.AddRangeAsync(products);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadSuppliers(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options); 
            
            if(suppliers is not null && suppliers.Count>0){
               await context.Suppliers.AddRangeAsync(suppliers);
               await context.SaveChangesAsync();
            } 
            
        }
        public static async Task LoadSupplierProducts(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.SupplierProducts.Any()) return;

            var json = File.ReadAllText("Data/json/supplierproducts.json");
            var suppliersproducts = JsonSerializer.Deserialize<List<SupplierProduct>>(json, options); 
            
            if(suppliersproducts is not null && suppliersproducts.Count>0){
               await context.SupplierProducts.AddRangeAsync(suppliersproducts);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadAddresses(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Addresses.Any()) return;

            var json = File.ReadAllText("Data/json/addresses.json");
            var addresess = JsonSerializer.Deserialize<List<Address>>(json, options); 
            
            if(addresess is not null && addresess.Count>0){
               await context.Addresses.AddRangeAsync(addresess);
               await context.SaveChangesAsync();
            } 
            
        }
        

        public static async Task LoadContactInformation(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.ContactInformations.Any()) return;

            var json = File.ReadAllText("Data/json/contactinformation.json");
            var contact = JsonSerializer.Deserialize<List<ContactInformation>>(json, options); 
            
            if(contact is not null && contact.Count>0){
               await context.ContactInformations.AddRangeAsync(contact);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadSupplierAddresses(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.SupplierAdresses.Any()) return;

            var json = File.ReadAllText("Data/json/supplieraddresses.json");
            var supplieraddress = JsonSerializer.Deserialize<List<SupplierAdress>>(json, options); 
            
            if(supplieraddress is not null && supplieraddress.Count>0){
               await context.SupplierAdresses.AddRangeAsync(supplieraddress);
               await context.SaveChangesAsync();
            } 
            
        }
        
    }

    
}