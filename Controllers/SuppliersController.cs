using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    public SuppliersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;


    }

    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        try
        {
            var suppliers = await _unitOfWork.supplierRepository.List();
            return Ok(new { success = true, data = suppliers });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
    [HttpGet("{name}")]
    public async Task<ActionResult> FindSupplier(string name)
    {
        try
        {
            var supplier = await _unitOfWork.supplierRepository.Find(name);
            if (supplier == null)
                return NotFound(new { success = false, message = "Supplier not found." });

            return Ok(new { success = true, data = supplier });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}
