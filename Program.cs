using bakery.api;
using bakery.api.Data;
using bakery.api.Interfaces;
using bakery.api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

//Knytta sammen app med DB
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
    //options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.LoadIngredients(context);
    await Seed.LoadSuppliers(context);
    await Seed.LoadSupplierIngredients(context);
    await Seed.LoadAddressTypes(context);
    await Seed.LoadPostalAddresses(context);
    await Seed.LoadAddresses(context);
    await Seed.LoadContactInformation(context);
    await Seed.LoadSupplierContactInformation(context);
    await Seed.LoadSupplierAddresses(context);
    await Seed.LoadCustomers(context);
    await Seed.LoadCustomerContactInformation(context);
    await Seed.LoadCustomerAddresses(context);
    await Seed.LoadOrders(context);
    await Seed.LoadProducts(context);
    await Seed.LoadOrderProducts(context);
    await Seed.LoadCustomerOrders(context);
    await Seed.LoadProductBatches(context);


}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}

app.MapControllers();

app.Run();
