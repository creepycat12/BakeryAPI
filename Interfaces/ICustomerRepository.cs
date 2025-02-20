using bakery.api.ViewModels;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Customers;

namespace bakery.api;

public interface ICustomerRepository
{
    public Task<IList<CustomersViewModel>> List();
    public Task<CustomerViewModel> Find(int id);
    public Task<bool> Add(AddCustomerForRepViewModel model);
    public Task<bool> Update(int id, ContactBaseViewModel model);

}
