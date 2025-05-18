using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Address;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Customers;
using bakery.api.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    private readonly IAddressRepository _repo;

    public CustomerRepository(DataContext context, IAddressRepository repo)
    {
        _repo = repo;
        _context = context;

    }

    public async Task<bool> Add(AddCustomerForRepViewModel model)
    {
        try
        {
            if (await _context.Customers.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower()) is not null)
            {
                throw new Exception($"The customer already exists");
            }

            var customer = new Customer
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl
            };

            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();

            var contact = await _context.Contact.FirstOrDefaultAsync(c => c.Email == model.Contacts.Email);
            if (contact is not null)
            {
                throw new Exception("Contact already exists");
            }
            else
            {
                contact = new Contact
                {
                    ContactPerson = model.Contacts.ContactPerson,
                    Email = model.Contacts.Email,
                    PhoneNumber = model.Contacts.PhoneNumber
                };
                await _context.Contact.AddAsync(contact);
                await _context.SaveChangesAsync();

            }

            var customerContact = new CustomerContact
            {
                CustomerId = customer.Id,
                ContactId = contact.ContactId
            };

            await _context.CustomerContacts.AddAsync(customerContact);


            foreach (var a in model.Addresses)
            {
                var address = await _repo.Add(a);

                await _context.CustomerAddresses.AddAsync(new CustomerAddress
                {
                    Address = address,
                    Customer = customer

                });
            }
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding customer: {ex.Message}");
            return false;

        }
    }

    public async Task<CustomerViewModel> Find(int id)

    {
        try
        {
            var customer = await _context.Customers
            .Where(c => c.Id == id)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.AddressType)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.PostalAddress)
            .Include(c => c.CustomerContact)
                .ThenInclude(c => c.Contact)
            .Include(c => c.CustomerOrders)
                .ThenInclude(c => c.Order)
                .ThenInclude(c => c.OrderProducts)
                .ThenInclude(c => c.Products)
            .SingleOrDefaultAsync();

            var view = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                ImageUrl = customer.ImageUrl,
            };

            var addresses = customer.CustomerAddresses.Select(c => new AddressViewModel
            {
                AddressLine = c.Address.AddressLine,
                AddressType = c.Address.AddressType.Value,
                City = c.Address.PostalAddress.City,
                PostalCode = c.Address.PostalAddress.PostalCode
            })
            .ToList();
            view.Addresses = addresses;

            var contacts = new ContactViewModel
            {
                ContactId = customer.CustomerContact.Contact.ContactId,
                ContactPerson = customer.CustomerContact.Contact.ContactPerson,
                Email = customer.CustomerContact.Contact.Email,
                PhoneNumber = customer.CustomerContact.Contact.PhoneNumber
            };
            view.Contacts = contacts;

            var orders = customer.CustomerOrders
        .Select(c => new OrdersViewModel
        {
            OrderNumber = c.Order.OrderNumber,
            OrderDate = c.Order.OrderDate,
            Customer = customer.Name,
            Products = c.Order.OrderProducts.Select(op => new OrderProductViewModel
            {
                Name = op.Products.Name,
                PackPrice = op.Products.PackPrice,
                AmountInPack = op.Products.AmountInPack,
                Weight_kg = op.Products.Weight_kg,
                PriceEach = op.Products.PackPrice / op.Products.AmountInPack,
                Quantity = op.Quantity
            }).ToList(),
            Total = c.Order.OrderProducts.Sum(op => op.Products.PackPrice * op.Quantity)
        })
        .ToList();
            view.Orders = orders;

            return view;

        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching customer: {ex.Message}");

        }
    }


    public async Task<IList<CustomersViewModel>> List()
    {
        var response = await _context.Customers
        .Include(c => c.CustomerContact)
        .ThenInclude(c => c.Contact)
        .ToListAsync();

        var customers = response.Select(c => new CustomersViewModel
        {
            Id = c.Id,
            Name = c.Name,
            ContactPerson = c.CustomerContact.Contact.ContactPerson,
            Email = c.CustomerContact.Contact.Email,
            PhoneNumber = c.CustomerContact.Contact.PhoneNumber,
            ImageUrl = c.ImageUrl,
        });

        return customers.ToList();
    }

    public async Task<bool> Update(int id, ContactBaseViewModel model)
    {
        try
        {
        var customer = await _context.Customers
        .Include (c => c.CustomerContact)
        .SingleOrDefaultAsync(c => c.Id == id);

        if(customer is null)
        {
            throw new Exception($"We dont have a client with ID {id}");
        }

        var contact = new Contact
        {
            ContactId = customer.CustomerContact.ContactId,
            ContactPerson = model.ContactPerson,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email
        };

        _context.Contact.Update(contact);
        await _context.SaveChangesAsync();
        return true;

        }
        catch(Exception)
        {
            throw;
        }
    
    }
}
