using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    
        private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
            _unitOfWork = unitOfWork;
       
    }

    [HttpGet()]
    public async Task<IActionResult> ListProducts()
    {
        return Ok(new { success = true, data = await _unitOfWork.ProductRepository.ListAllProducts() });

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProducts(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.GetProducts(id);
            return Ok(new { success = true, data = product });
        }
        catch (Exception ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }

    [HttpPost("/api/Products/Add")]
    public async Task<ActionResult> AddProduct(ProductPostViewModel model)
    {
        if(await _unitOfWork.ProductRepository.AddProduct(model))
        {
            if (_unitOfWork.HasChanges())
            {
                await _unitOfWork.Complete();
            }
            return StatusCode(201);
        }
        else
        {
            return BadRequest();
        }
    }

   [HttpPatch("/api/Products/{id}/Price/{price}")]
    public async Task<ActionResult> UpdatePrice(int id, decimal price)
    {
        if( await _unitOfWork.ProductRepository.Update(id, price))
        {
            if(_unitOfWork.HasChanges())
            {
                await _unitOfWork.Complete();
            }

            return StatusCode(201);
        }
        else
        {
            return BadRequest();
        }
    }
}



