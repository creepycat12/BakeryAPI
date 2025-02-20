using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Entities;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Contact;

namespace bakery.api.Interfaces;

public interface IContactRepository
{
    public Task<Contact> Add(ContactPostViewModel model);
}
