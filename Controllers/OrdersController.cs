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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    public OrdersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]

    public async Task<ActionResult> ListOrders()
    {
        try
        {
            var orders = await _unitOfWork.OrderRepository.List();
            if (orders is not null) return Ok(new { success = true, data = orders });
            return StatusCode(500);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }

    }

    [HttpGet("ordernumber/{orderNumber}")]
    public async Task<ActionResult> FindOrderNumber(string orderNumber)
    {
        try
        {
            var orders = await _unitOfWork.OrderRepository.Find(orderNumber);
            if (orders is null) return BadRequest(new { success = false, message = $"No orders found with ordernumber {orderNumber}" });
            return Ok(new { sucess = true, Data = orders });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    [HttpGet("date/{orderDate}")]
    public async Task<ActionResult> FindOrderDate(DateTime orderDate)
    {
        try
        {
            var orders = await _unitOfWork.OrderRepository.Find(orderDate);
            if (orders is null) return BadRequest(new { success = false, message = $"No orders found with Order Date{orderDate}" });
            return Ok(new { sucess = true, Data = orders });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddOrder(OrdersPostViewModel model)
    {
        if (await _unitOfWork.OrderRepository.Add(model))
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

}

