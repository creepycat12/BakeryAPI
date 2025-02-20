using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
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

        // [HttpGet()]
        // public async Task<ActionResult> ListAllSuppliers()
        // {
        //     var supplier = await _context.Suppliers
        //     .Include(s => s.SupplierIngredients)
        //     .ThenInclude(s=> s.Supplier.SupplierContact)
        //     .Include(s => s.SupplierIngredients)
        //     .ThenInclude(s=> s.Supplier.SupplierAdress)
        //     .Select(s => new
        //     {
        //         Company = s.Name,
        //         Adress = new
        //         {
        //             Steet = s.SupplierAdress.Address.AddressLine,
        //             City = s.SupplierAdress.Address.PostalAddress.City,
        //             PostalCode = s.SupplierAdress.Address.PostalAddress.PostalCode,
        //             ContactInformation = new
        //             {
        //                 Name = s.SupplierContact.Contact.Email,
        //                 //Name = s.SupplierContact.Contact.ContactPerson,
        //                 PhoneNumber = s.SupplierContact.Contact.PhoneNumber,
        //                 Email = s.SupplierContact.Contact.Email
        //             }
        //         },

        //         Products = s.SupplierIngredients
        //             .Select(sp => new
        //             {
        //                 sp.ingredient.ItemNumber,
        //                 sp.ingredient.IngredientName,
        //                 price = sp.ingredient.Price_Kg
        //             })
        //     })
        //     .ToListAsync();

        //     return Ok(new { success = true, StatusCode = 200, data = supplier });
        // }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindSupplier(string name)
        {

            var supplier = await _context.Suppliers
            .Where(s => s.Name.ToLower() == name.ToLower())
            .Include(p => p.SupplierIngredients)
            .Select(supplier => new
            {
                supplier.Name,
                products = supplier.SupplierIngredients
                .Select(prod => new
                {
                    prod.ingredient.IngredientName,
                    prod.ingredient.Price_Kg,
                    prod.ingredient.ItemNumber
                })
            })
            .FirstOrDefaultAsync();

            if (supplier == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"Supplier \"{name}\" not found." });
            }

            return Ok(new { success = true, StatusCode = 200, data = supplier });

        }
    }
