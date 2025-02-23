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
        try{
            return Ok(new { success = true, data = await _unitOfWork.ProductRepository.ListAllProducts() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
       
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

    [HttpPost("Add")]
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

   [HttpPatch("{id}/price/{price}")]
    public async Task<ActionResult> UpdatePrice(int id, decimal price)
    {
        try
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
            return BadRequest(new { success = false, message = $"Product with ID {id} not found" });
        }

        }
        catch(Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
       
    }
}



