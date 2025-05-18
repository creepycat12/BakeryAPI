using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bakery.api;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;
    public ProductRepository(DataContext context)
    {
        _context = context;

    }

    public async Task<bool> AddProduct(ProductPostViewModel model)
    {
        try
        {
            var newProduct = new Product
            {
                Name = model.ProductName,
                PackPrice = model.PackPrice,
                Weight_kg = model.Weight_kg,
                AmountInPack = model.AmountInPack,
                ImageUrl = model.ImageUrl,
                Description = model.Description
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<GetProductViewModel> GetProducts(int id)
    {
        try
        {

            var product = await _context.Products
                 .Where(c => c.Id == id)
                 .Include(c => c.ProductBatches)
                 .SingleOrDefaultAsync();

            var view = new GetProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                PackPrice = product.PackPrice,
                Weight_kg = product.Weight_kg,
                AmountInPack = product.AmountInPack,
                ImageUrl = product.ImageUrl,
                Description = product.Description
            };

            IList<BatchViewModel> batch = [];

            foreach (var c in product.ProductBatches)
            {
                var PB = new BatchViewModel
                {
                    PreperationDate = c.PreperationDate,
                    ExpiryDate = c.ExpiryDate

                };

                batch.Add(PB);
            }
            view.Batches = batch;
            return view;

        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong {ex.Message}");
        }
    }

    public async Task<IList<ProductsViewModel>> ListAllProducts()
    {
        var products = await _context.Products.ToListAsync();

        IList<ProductsViewModel> response = [];

        foreach (var product in products)
        {
            var view = new ProductsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                PackPrice = product.PackPrice,
                Weight_kg = product.Weight_kg,
                AmountInPack = product.AmountInPack,
                ImageUrl = product.ImageUrl,
                Description = product.Description
            };
            response.Add(view);
        }
        return response;
    }

    public async Task<bool> Update(int id, decimal price)
    {
        try
        {
            var product = await _context.Products.SingleOrDefaultAsync(c => c.Id == id);

            if (product is null)
            {
                throw new Exception($"Product with Id {id} does not exist");

            }
            product.PackPrice = price;

            await _context.SaveChangesAsync();
            return true;

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        

    }
}

