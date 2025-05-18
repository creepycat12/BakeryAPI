using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Entities;
using bakery.api.ViewModels;

namespace bakery.api.Interfaces;

public interface IAddressRepository
{
    public Task<Address> Add(AddressPostViewModel model);
}
