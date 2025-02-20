using bakery.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Supplier> Suppliers{ get; set; }
        public DbSet<SupplierIngredient> SupplierIngredients{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SupplierContact> SupplierContact { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }





        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplierIngredient>().HasKey(o => new{o.IngredientId, o.SupplierId});
            modelBuilder.Entity<Contact>().HasKey(o => new{o.ContactId});
            modelBuilder.Entity<SupplierContact>().HasKey(o=>new{o.ContactId, o.SupplierId});
            // modelBuilder.Entity<ContactInformation>()
            //     .Property(o => o.SupplierId)
               //.ValueGeneratedNever();
            modelBuilder.Entity<SupplierAddress>().HasKey(o => new{o.SupplierId, o.AddressId});
            modelBuilder.Entity<CustomerAddress>().HasKey(o => new{o.CustomerId, o.AddressId});
            modelBuilder.Entity<CustomerContact>().HasKey(o => new{o.CustomerId, o.ContactId});
            modelBuilder.Entity<CustomerOrder>().HasKey(o => new{o.CustomerId, o.OrderId});
            modelBuilder.Entity<OrderProduct>().HasKey(o => new{o.ProductId, o.OrderId});
        }
        
    }
}