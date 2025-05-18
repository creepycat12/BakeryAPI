using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Contact;
using bakery.api.ViewModels.Customers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    public CustomersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    [HttpGet]
    public async Task<ActionResult> ListAllCustomers()
    {
        var customers = await _unitOfWork.CustomerRepository.List();
        return Ok(new { success = true, data = customers });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCustomer(int id)
    {
        try
        {
            return Ok(new { sucess = true, Data = await _unitOfWork.CustomerRepository.Find(id) });
        }
        catch (Exception ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }

    }

    [HttpPost ("/api/Customers/Add")]
    public async Task<ActionResult> AddCustomer(AddCustomerForRepViewModel model)
    {
    
            if(await _unitOfWork.CustomerRepository.Add(model))
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
    
    [HttpPut("/updateContact/{id}")]
    public async Task<ActionResult> UpdateContactPerson(int id, ContactBaseViewModel model)
    {
        if(await _unitOfWork.CustomerRepository.Update(id, model))
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





