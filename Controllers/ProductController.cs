using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet()]
        public async Task<ActionResult> ListAllProducts()
        {
            var result = await _context.Products
            .Include(c => c.SupplierProducts)
            .Select(c => new
            {
                c.ItemNumber,
                c.ProductName,
                c.Price_Kg,
                SupplierInformation = c.SupplierProducts
                    .Select(si => new
                    {
                        si.Supplier.Name,
                        si.Supplier.ContactInformation.ContactPerson,
                        si.Supplier.ContactInformation.PhoneNumber,
                        si.Supplier.ContactInformation.Email,
                        si.Supplier.SupplierAdress.Address.Street,
                        si.Supplier.SupplierAdress.Address.StreetNr,
                        si.Supplier.SupplierAdress.Address.City,
                        si.Supplier.SupplierAdress.Address.PostalCode
                    })

            })
            .ToListAsync();
            
            if (!result.Any())
        {
            return NotFound(new { success = false, StatusCode = 404, message = "No products found" });
        }

            return Ok(new { success = true, StatusCode = 200, data = result });
        }


        [HttpGet("{name}")]
        public async Task<ActionResult> FindProducts(string name)
        {

            var product = await _context.Products
            .Where(p => p.ProductName.ToLower() == name.ToLower())
            .Include(sp => sp.SupplierProducts)
            .Select(product => new
            {
                product.ProductName,
                product.Price_Kg,
                suppliers = product.SupplierProducts
                .Select(SupplierProduct => new
                {
                    Company = SupplierProduct.Supplier.Name,
                    Address = new
                    {
                        Street = SupplierProduct.Supplier.SupplierAdress.Address.Street,
                        StreetNr = SupplierProduct.Supplier.SupplierAdress.Address.StreetNr,
                        City = SupplierProduct.Supplier.SupplierAdress.Address.City,
                        PostalCode = SupplierProduct.Supplier.SupplierAdress.Address.PostalCode,
                        ContactPerson = new
                        {
                            ContactName = SupplierProduct.Supplier.ContactInformation.ContactPerson,
                            PhoneNumber = SupplierProduct.Supplier.ContactInformation.PhoneNumber,
                            Email = SupplierProduct.Supplier.ContactInformation.Email,
                        }
                    }
                })
            })
            .ToListAsync();

            if (!product.Any())
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"No products found with the name \"{name}\" " });
            }
            return Ok(new { success = true, StatusCode = 200, data = product });
        }


        [HttpPost("AddProducts/{id}")]
        public async Task<ActionResult> AddProduct(int id, ProductsViewModels model)
        {
            var supplier = await _context.Suppliers
             .Include(s => s.SupplierProducts)
             .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"Supplier with ID {id} not found." });
            }

            var product = new Product
            {
                ItemNumber = model.ItemNumber,
                ProductName = model.ProductName,
                Price_Kg = model.Price_Kg
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var supplierProduct = new SupplierProduct
            {
                SupplierId = id,
                ProductId = product.ProductId
            };

            await _context.SupplierProducts.AddAsync(supplierProduct);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(AddProduct), new {id = product.ProductId});
    
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePrice(int id, [FromQuery] decimal price)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (prod == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"Product with id {id} doesnt exist" });
            }

            prod.Price_Kg = price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

            return NoContent();
        }

    }
}