using bakery.api.ViewModels;

namespace bakery.api;

public interface IProductRepository
{
    public Task<IList<ProductsViewModel>> ListAllProducts();
    public Task<GetProductViewModel> GetProducts(int id);
    public Task<bool> AddProduct(ProductPostViewModel model);
    public Task<bool> Update(int id, decimal price);


}
