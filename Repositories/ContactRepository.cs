using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Repositories;

public class ContactRepository : IContactRepository
{
        private readonly DataContext _context;
    public ContactRepository(DataContext context)
    {
            _context = context;
        
    }
    public async Task<Contact> Add(ContactPostViewModel model)
    {
        var customerContact = await _context.CustomerContacts.FirstOrDefaultAsync(
            c => c.ContactId == model.ContactId
        );

        if(customerContact is null)
        {
            customerContact = new CustomerContact{
                ContactId = model.ContactId,
                CustomerId = model.CustomerId
            };

            await _context.CustomerContacts.AddAsync(customerContact);
        }

        var contact = await _context.Contact.FirstOrDefaultAsync(
            c => c.Email.ToLower().Trim() == model.Email.ToLower().Trim()
        );

        if(contact is null)
        {
            contact = new Contact{
                ContactPerson = model.ContactPerson.ToLower().Trim(),
                Email = model.Email.ToLower().Trim(),
                PhoneNumber = model.PhoneNumber.Replace(" ",""),
                CustommerContacts = customerContact
            };
            await _context.Contact.AddAsync(contact);
        }
        await _context.SaveChangesAsync();
        return contact;
    }
    
}
