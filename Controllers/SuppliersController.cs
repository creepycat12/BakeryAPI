using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly DataContext _context;
        public SuppliersController(DataContext context)
        {
            _context = context;

        }

        [HttpGet()]
        public async Task<ActionResult> ListAllSuppliers()
        {
            var supplier = await _context.Suppliers
            .Include(s => s.SupplierProducts)
            .Select(s => new
            {
                Company = s.Name,
                Adress = new
                {
                    Steet = s.SupplierAdress.Address.Street,
                    SteetNr = s.SupplierAdress.Address.StreetNr,
                    City = s.SupplierAdress.Address.City,
                    PostalCode = s.SupplierAdress.Address.PostalCode,
                    ContactInformation = new
                    {
                        Name = s.ContactInformation.ContactPerson,
                        PhoneNumber = s.ContactInformation.PhoneNumber,
                        Email = s.ContactInformation.Email
                    }
                },

                Products = s.SupplierProducts
                    .Select(sp => new
                    {
                        sp.Product.ItemNumber,
                        sp.Product.ProductName,
                        price = sp.Product.Price_Kg
                    })
            })
            .ToListAsync();

            return Ok(new { success = true, StatusCode = 200, data = supplier });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindSupplier(string name)
        {

            var supplier = await _context.Suppliers
            .Where(s => s.Name.ToLower() == name.ToLower())
            .Include(p => p.SupplierProducts)
            .Select(supplier => new
            {
                supplier.Name,
                products = supplier.SupplierProducts
                .Select(prod => new
                {
                    prod.Product.ProductName,
                    prod.Product.Price_Kg,
                    prod.Product.ItemNumber
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
}