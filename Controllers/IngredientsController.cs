using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// namespace bakery.api.Controllers
// {
//     [ApiController]
//     [Route("api/ingredients")]
//     public class IngredientsController(DataContext context) : ControllerBase
//     {
//         private readonly DataContext _context = context;

//         [HttpGet()]
//         public async Task<ActionResult> ListAllIngredients()
//         {
//             var result = await _context.Ingredients
//             .Include(c => c.SupplierIngredients)
//             .ThenInclude(c => c.Supplier.SupplierContact)
//             .Include(c => c.SupplierIngredients)
//             .ThenInclude(c => c.Supplier.SupplierAdress)
//             .Select(c => new
//             {
//                 c.ItemNumber,
//                 c.IngredientName,
//                 c.Price_Kg,
//                 SupplierInformation = c.SupplierIngredients
//                     .Select(si => new
//                     {
//                         si.Supplier.Name,
//                         si.Supplier.SupplierContact.Contact.ContactPerson,
//                         si.Supplier.SupplierContact.Contact.PhoneNumber,
//                         si.Supplier.SupplierContact.Contact.Email,
//                         si.Supplier.SupplierAdress.Address.AddressLine,
//                         si.Supplier.SupplierAdress.Address.PostalAddress.City,
//                         si.Supplier.SupplierAdress.Address.AddressType.Value
//                     })

//             })
//             .ToListAsync();
            
//             if (!result.Any())
//         {
//             return NotFound(new { success = false, StatusCode = 404, message = "No ingredients found" });
//         }

//             return Ok(new { success = true, StatusCode = 200, data = result });
//         }


//         [HttpGet("{name}")]
//         public async Task<ActionResult> FindIngredients(string name)
//         {

//             var ingredient = await _context.Ingredients
//             .Where(p => p.IngredientName.ToLower() == name.ToLower())
//             .Include(sp => sp.SupplierIngredients)
//             .Select(product => new
//             {
//                 product.IngredientName,
//                 product.Price_Kg,
//                 suppliers = product.SupplierIngredients
//                 .Select(SupplierIngredient => new
//                 {
//                     Company = SupplierIngredient.Supplier.Name,
//                     Address = new
//                     {
//                         Street = SupplierIngredient.Supplier.SupplierAdress.Address.AddressLine,
//                         City = SupplierIngredient.Supplier.SupplierAdress.Address.PostalAddress.City,
//                         PostalCode = SupplierIngredient.Supplier.SupplierAdress.Address.PostalAddress.PostalCode,
//                         ContactPerson = new
//                         {
//                             ContactName = SupplierIngredient.Supplier.SupplierContact.Contact.ContactPerson,
//                             PhoneNumber = SupplierIngredient.Supplier.SupplierContact.Contact.PhoneNumber,
//                             Email = SupplierIngredient.Supplier.SupplierContact.Contact.Email,
//                         }
//                     }
//                 })
//             })
//             .ToListAsync();

//             if (!ingredient.Any())
//             {
//                 return NotFound(new { success = false, StatusCode = 404, message = $"No products found with the name \"{name}\" " });
//             }
//             return Ok(new { success = true, StatusCode = 200, data = ingredient });
//         }


//         [HttpPost("AddProducts/{SupplierId}")]
//         public async Task<ActionResult> AddProduct(int SupplierId, IngredientPostViewModel model)
//         {
//             var supplier = await _context.Suppliers
//              .Include(s => s.SupplierIngredients)
//              .FirstOrDefaultAsync(s => s.SupplierId == SupplierId);

//             if (supplier == null )
//             {
//                 return NotFound(new { success = false, StatusCode = 404, message = $"Supplier with ID {SupplierId} not found." });
//             }

//             var ingredient = new Ingredient
//             {
//                 ItemNumber = model.ItemNumber,
//                 IngredientName = model.IngredientName,
//                 Price_Kg = model.Price_Kg
//             };

//             await _context.Ingredients.AddAsync(ingredient);
//             await _context.SaveChangesAsync();

//             var supplierIngredient = new SupplierIngredient
//             {
//                 SupplierId = SupplierId,
//                 IngredientId = ingredient.IngredientId
//             };

//             await _context.SupplierIngredients.AddAsync(supplierIngredient);
//             await _context.SaveChangesAsync();


//             return CreatedAtAction(nameof(AddProduct), new {SupplierId = ingredient.IngredientId}, 
//             new{ success = true, Message = $"Product successfully added to Supplier"});
//         }

//         [HttpPatch("{id}")]
//         public async Task<ActionResult> UpdatePrice(int id, [FromQuery] decimal price)
//         {
//             var ingr = await _context.Ingredients.FirstOrDefaultAsync(p => p.IngredientId == id);

//             if (ingr == null)
//             {
//                 return NotFound(new { success = false, StatusCode = 404, message = $"Product with id {id} doesnt exist" });
//             }

//             ingr.Price_Kg = price;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, ex.Message);

//             }

//             return NoContent();
//         }

//     }
// }