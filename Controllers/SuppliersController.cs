using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly DataContext _context;
    public SuppliersController(DataContext context)
    {
        _context = context;

    }

    [HttpGet]
    public async Task<ActionResult> GetSuppliers()
    {
        var suppliers = await _context.Suppliers
            .Include(c => c.SupplierContact)
                .ThenInclude(c => c.Contact)
            .Include(c => c.Addresses)
            .Select(supplier => new SuppliersViewModel
            {
                Name = supplier.Name,
                Id = supplier.Id,
                ImageUrl = supplier.ImageUrl,
                AddressLine = supplier.Addresses.FirstOrDefault().Address.AddressLine,
                City = supplier.Addresses.FirstOrDefault().Address.PostalAddress.City,
                PhoneNumber = supplier.SupplierContact.Contact.PhoneNumber,
                Postalcode = supplier.Addresses.FirstOrDefault().Address.PostalAddress.PostalCode,
                Email = supplier.SupplierContact.Contact.Email
            }).ToListAsync();
        if (suppliers == null)
        {
            return NotFound(new { success = false, StatusCode = 404, message = "No suppliers found." });
        }
        return Ok(new { success = true, StatusCode = 200, data = suppliers });

    }


    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {

        var supplier = await _context.Suppliers
        .Where(s => s.Id == id)
         .Include(c => c.SupplierContact)
             .ThenInclude(c => c.Contact)
         .Include(c => c.Addresses)
         .Include(c => c.SupplierIngredients)
         .ThenInclude(c => c.ingredient)
         .Select(supplier => new SupplierViewModel
         {
             Name = supplier.Name,
             Id = supplier.Id,
             ImageUrl = supplier.ImageUrl,
             AddressLine = supplier.Addresses.FirstOrDefault().Address.AddressLine,
             City = supplier.Addresses.FirstOrDefault().Address.PostalAddress.City,
             PhoneNumber = supplier.SupplierContact.Contact.PhoneNumber,
             Postalcode = supplier.Addresses.FirstOrDefault().Address.PostalAddress.PostalCode,
             Email = supplier.SupplierContact.Contact.Email,
             ContactPerson = supplier.SupplierContact.Contact.ContactPerson,
             Ingredients = supplier.SupplierIngredients.Select(i => new IngredientViewModel
             {
                 name = i.ingredient.IngredientName,
                 Price_Kg = i.ingredient.Price_Kg
             }).ToList()
         })

             .FirstOrDefaultAsync();


        if (supplier == null)
        {
            return NotFound(new { success = false, StatusCode = 404, message = $"Supplier  not found." });
        }

        return Ok(new { success = true, StatusCode = 200, data = supplier });

    }

    [HttpPost("/api/Suppliers/Add")]
    public async Task<ActionResult> AddSupplier(SupplierPostViewModel supplierPostViewModel)
    {
        var postalAddress = new PostalAddress
        {
            City = supplierPostViewModel.City,
            PostalCode = supplierPostViewModel.Postalcode
        };

        await _context.PostalAddresses.AddAsync(postalAddress);
        await _context.SaveChangesAsync(); 

        var address = new Address
        {
            AddressLine = supplierPostViewModel.AddressLine,
            AddressTypeId = (int)AddressTypeEnum.Delivery, 
            PostalAddressId = postalAddress.Id
        };

        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();

       
        var supplier = new Supplier
        {
            Name = supplierPostViewModel.Name,
            ImageUrl = supplierPostViewModel.ImageUrl,
            SupplierContact = new SupplierContact
            {
                Contact = new Contact
                {
                    PhoneNumber = supplierPostViewModel.PhoneNumber,
                    Email = supplierPostViewModel.Email,
                    ContactPerson = supplierPostViewModel.ContactPerson
                },
            },
            Addresses = new List<SupplierAddress>
                {
                    new SupplierAddress
                    {
                        AddressId = address.AddressId
                    }
                }
        };

        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, StatusCode = 200, message = "Supplier added successfully." });
    }

}
