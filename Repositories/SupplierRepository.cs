using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Address;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Supplier;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly DataContext _context;
    public SupplierRepository(DataContext context)
    {
        _context = context;

    }

    public async Task<SupplierViewModel> Find(string name)
    {
        try
        {
            var supplier = await _context.Suppliers
                .Where(c => c.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower())
                .Include(c => c.SupplierIngredients)
                    .ThenInclude(si => si.ingredient)
                .Include(c => c.SupplierAddresses)
                    .ThenInclude(sa => sa.Address)
                .Include(c => c.SupplierContact)
                    .ThenInclude(sc => sc.Contact)
                .Select(s => new SupplierViewModel
                {
                    SupplierName = s.Name,
                    Address = s.SupplierAddresses
                        .Select(sa => new AddressViewModel
                        {
                            AddressLine = sa.Address.AddressLine,
                            City = sa.Address.PostalAddress.City,
                            PostalCode = sa.Address.PostalAddress.PostalCode,
                            AddressType = sa.Address.AddressType.Value
                        })
                        .FirstOrDefault(),
                    Contact = new ContactBaseViewModel
                    {
                        ContactPerson = s.SupplierContact.Contact.ContactPerson,
                        Email = s.SupplierContact.Contact.Email,
                        PhoneNumber = s.SupplierContact.Contact.PhoneNumber
                    },
                    Ingredients = s.SupplierIngredients
                        .Select(si => new IngredientsBaseViewModel
                        {
                            Name = si.ingredient.IngredientName,
                            Price_Kg = si.ingredient.Price_Kg,
                            ItemNumber = si.ingredient.ItemNumber
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (supplier == null)
                throw new Exception($"Supplier '{name}' not found.");

            return supplier;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the supplier.", ex);
        }
    }

    public async Task<IList<SupplierBaseViewModel>> List()
    {
        try
        {
            var suppliers = await _context.Suppliers
                .Include(c => c.SupplierIngredients)
                .Select(c => new SupplierBaseViewModel
                {
                    SupplierName = c.Name,
                    Ingredients = c.SupplierIngredients
                        .Select(c => new IngredientsBaseViewModel
                        {
                            Name = c.ingredient.IngredientName,
                            Price_Kg = c.ingredient.Price_Kg,
                            ItemNumber = c.ingredient.ItemNumber
                        }).ToList()
                }).ToListAsync();

            return suppliers;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the supplier list.", ex);
        }
    }
}
