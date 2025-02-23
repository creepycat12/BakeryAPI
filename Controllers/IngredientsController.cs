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
    [Route("api/ingredients")]
    public class IngredientsController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult> ListAllIngredients()
        {
            try
            {
                var result = await _context.Ingredients
                    .Include(c => c.SupplierIngredients)
                    .Select(c => new
                    {
                        c.IngredientName,
                        c.IngredientId,
                        c.Price_Kg,
                        SupplierInformation = c.SupplierIngredients
                            .Select(c => new { SupplierName = c.Supplier.Name })
                    }).ToListAsync();

                if (!result.Any())
                {
                    return NotFound(new { success = false, statusCode = 404, message = "No ingredients found." });
                }

                return Ok(new { success = true, statusCode = 200, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, statusCode = 500, message = "An error occurred while getting ingredients.", ex.Message });
            }
        }


        [HttpGet("{name}")]
        public async Task<ActionResult> FindIngredients(string name)
        {
            try
            {
                var ingredient = await _context.Ingredients
                    .Where(p => p.IngredientName.ToLower() == name.ToLower())
                    .Include(sp => sp.SupplierIngredients)
                    .Select(product => new
                    {
                        product.IngredientId,
                        product.IngredientName,
                        product.Price_Kg,
                        SupplierInfo = product.SupplierIngredients
                            .Select(c => new
                            {
                                SupplierName = c.Supplier.Name,
                                ContactPerson = c.Supplier.SupplierContact.Contact.ContactPerson,
                                Phone = c.Supplier.SupplierContact.Contact.PhoneNumber,
                                Email = c.Supplier.SupplierContact.Contact.Email,
                                SupplierAddress = c.Supplier.SupplierAddresses
                                    .Select(c => new
                                    {
                                        c.Address.AddressLine,
                                        c.Address.PostalAddress.City,
                                        c.Address.PostalAddress.PostalCode,
                                        c.Address.AddressType.Value
                                    })
                            })
                    })
                    .ToListAsync();

                if (!ingredient.Any())
                {
                    return NotFound(new { success = false, statusCode = 404, message = $"No ingredients found with name {name}" });
                }

                return Ok(new { success = true, statusCode = 200, data = ingredient });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, statusCode = 500, message = "An error occurred while searching for the ingredient.", error = ex.Message });
            }
        }

        [HttpPost("AddProducts/{SupplierId}")]
        public async Task<ActionResult> AddProduct(int SupplierId, IngredientPostViewModel model)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .Include(s => s.SupplierIngredients)
                    .FirstOrDefaultAsync(s => s.SupplierId == SupplierId);

                if (supplier == null)
                {
                    return NotFound(new { success = false, statusCode = 404, message = $"Supplier with ID {SupplierId} not found." });
                }

                var ingredient = new Ingredient
                {
                    ItemNumber = model.ItemNumber,
                    IngredientName = model.IngredientName,
                    Price_Kg = model.Price_Kg
                };

                await _context.Ingredients.AddAsync(ingredient);
                await _context.SaveChangesAsync();

                var supplierIngredient = new SupplierIngredient
                {
                    SupplierId = SupplierId,
                    IngredientId = ingredient.IngredientId
                };

                await _context.SupplierIngredients.AddAsync(supplierIngredient);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(AddProduct), new { ingredientId = ingredient.IngredientId },
                    new { success = true, message = "Product successfully added to supplier." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, statusCode = 500, message = "An error occurred while adding the ingredient.", error = ex.Message });
            }
        }

        [HttpPatch("ChangePrice/{id}")]
        public async Task<ActionResult> UpdatePrice(int id, [FromQuery] decimal price)
        {
            try
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(p => p.IngredientId == id);

                if (ingredient == null)
                {
                    return NotFound(new { success = false, statusCode = 404, message = $"Ingredient with ID {id} does not exist." });
                }

                ingredient.Price_Kg = price;
                await _context.SaveChangesAsync();

                return Ok(new { success = true, statusCode = 200, message = "Ingredient price updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, statusCode = 500, message = "An error occurred while updating the ingredient price.", error = ex.Message });
            }
        }
    }
}