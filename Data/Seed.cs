using System.Text.Json;
using bakery.api.Entities;
namespace bakery.api.Data
{
    public static class Seed
    {
        public static async Task LoadIngredients(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Ingredients.Any()) return;

            var json = File.ReadAllText("Data/json/ingredients.json");
            var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json, options); 
            
            if(ingredients is not null && ingredients.Count>0){
               await context.Ingredients.AddRangeAsync(ingredients);
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
         public static async Task LoadCustomers(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Customers.Any()) return;

            var json = File.ReadAllText("Data/json/customer.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(json, options); 
            
            if(customers is not null && customers.Count>0){
               await context.Customers.AddRangeAsync(customers);
               await context.SaveChangesAsync();
            } 
            
        }
        public static async Task LoadSupplierIngredients(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.SupplierIngredients.Any()) return;

            var json = File.ReadAllText("Data/json/supplieringredients.json");
            var suppliersingredients = JsonSerializer.Deserialize<List<SupplierIngredient>>(json, options); 
            
            if(suppliersingredients is not null && suppliersingredients.Count>0){
               await context.SupplierIngredients.AddRangeAsync(suppliersingredients);
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

         public static async Task LoadAddressTypes(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.AddressTypes.Any()) return;

            var json = File.ReadAllText("Data/json/addresstypes.json");
            var addresstypes = JsonSerializer.Deserialize<List<AddressType>>(json, options); 
            
            if(addresstypes is not null && addresstypes.Count>0){
               await context.AddressTypes.AddRangeAsync(addresstypes);
               await context.SaveChangesAsync();
            } 
            
        }


         public static async Task LoadPostalAddresses(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.PostalAddresses.Any()) return;

            var json = File.ReadAllText("Data/json/postaladdress.json");
            var postaladdress = JsonSerializer.Deserialize<List<PostalAddress>>(json, options); 
            
            if(postaladdress is not null && postaladdress.Count>0){
               await context.PostalAddresses.AddRangeAsync(postaladdress);
               await context.SaveChangesAsync();
            } 
            
        }
        

        public static async Task LoadContactInformation(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Contact.Any()) return;

            var json = File.ReadAllText("Data/json/contactinformation.json");
            var contact = JsonSerializer.Deserialize<List<Contact>>(json, options); 
            
            if(contact is not null && contact.Count>0){
               await context.Contact.AddRangeAsync(contact);
               await context.SaveChangesAsync();
            } 
            
        }


        public static async Task LoadSupplierContactInformation(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.SupplierContact.Any()) return;

            var json = File.ReadAllText("Data/json/suppliercontact.json");
            var contact = JsonSerializer.Deserialize<List<SupplierContact>>(json, options); 
            
            if(contact is not null && contact.Count>0){
               await context.SupplierContact.AddRangeAsync(contact);
               await context.SaveChangesAsync();
            } 
            
        }

         public static async Task LoadCustomerContactInformation(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.CustomerContacts.Any()) return;

            var json = File.ReadAllText("Data/json/customercontact.json");
            var contact = JsonSerializer.Deserialize<List<CustomerContact>>(json, options); 
            
            if(contact is not null && contact.Count>0){
               await context.CustomerContacts.AddRangeAsync(contact);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadSupplierAddresses(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.SupplierAddresses.Any()) return;

            var json = File.ReadAllText("Data/json/supplieraddresses.json");
            var supplieraddress = JsonSerializer.Deserialize<List<SupplierAddress>>(json, options); 
            
            if(supplieraddress is not null && supplieraddress.Count>0){
               await context.SupplierAddresses.AddRangeAsync(supplieraddress);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadCustomerAddresses(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.CustomerAddresses.Any()) return;

            var json = File.ReadAllText("Data/json/customeraddresses.json");
            var customeraddress = JsonSerializer.Deserialize<List<CustomerAddress>>(json, options); 
            
            if(customeraddress is not null && customeraddress.Count>0){
               await context.CustomerAddresses.AddRangeAsync(customeraddress);
               await context.SaveChangesAsync();
            } 
            
        }


        public static async Task LoadOrders(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.Orders.Any()) return;

            var json = File.ReadAllText("Data/json/orders.json");
            var orders = JsonSerializer.Deserialize<List<Order>>(json, options); 
            
            if(orders is not null && orders.Count>0){
               await context.Orders.AddRangeAsync(orders);
               await context.SaveChangesAsync();
            } 
            
        }

         public static async Task LoadCustomerOrders(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.CustomerOrders.Any()) return;

            var json = File.ReadAllText("Data/json/customerorders.json");
            var orders = JsonSerializer.Deserialize<List<CustomerOrder>>(json, options); 
            
            if(orders is not null && orders.Count>0){
               await context.CustomerOrders.AddRangeAsync(orders);
               await context.SaveChangesAsync();
            } 
            
        }

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

         public static async Task LoadOrderProducts(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.OrderProducts.Any()) return;

            var json = File.ReadAllText("Data/json/orderproducts.json");
            var orders = JsonSerializer.Deserialize<List<OrderProduct>>(json, options); 
            
            if(orders is not null && orders.Count>0){
               await context.OrderProducts.AddRangeAsync(orders);
               await context.SaveChangesAsync();
            } 
            
        }

        public static async Task LoadProductBatches(DataContext context){
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive=true

            };

            if(context.ProductBatches.Any()) return;

            var json = File.ReadAllText("Data/json/productbatches.json");
            var batches = JsonSerializer.Deserialize<List<ProductBatch>>(json, options); 
            
            if(batches is not null && batches.Count>0){
               await context.ProductBatches.AddRangeAsync(batches);
               await context.SaveChangesAsync();
            } 
            
        }
        

        
    }

    
}