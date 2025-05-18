using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Interfaces;
using bakery.api.Repositories;

namespace bakery.api.Data;

public class UnitOfWork(DataContext context, IAddressRepository repo) : IUnitOfWork
{
    private readonly DataContext _context = context;
    private readonly IAddressRepository _repo = repo;
    public IAddressRepository AddressRepository => new AddressRepository(_context);

    public IBatchRepository BatchRepository => new BatchRepository(_context);

    public IContactRepository ContactRepository => new ContactRepository (_context);

    public ICustomerRepository CustomerRepository => new CustomerRepository(_context, _repo);

    public IOrderRepository OrderRepository => new OrderRepository(_context, _repo);

    public IProductRepository ProductRepository => new ProductRepository(_context);
    
    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}
