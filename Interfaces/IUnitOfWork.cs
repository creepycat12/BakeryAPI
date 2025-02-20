using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Interfaces;

public interface IUnitOfWork
{
    IAddressRepository AddressRepository{ get; }
    IBatchRepository BatchRepository { get; }
    IContactRepository ContactRepository { get;}
    ICustomerRepository CustomerRepository { get;}
    IOrderRepository OrderRepository {get;}
    IProductRepository ProductRepository {get;}

    Task <bool> Complete();
    bool HasChanges();

}
