using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.Repositories;
using bakery.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BatchController : ControllerBase
{
        private readonly IUnitOfWork _unitOfWork;
        
    public BatchController(IUnitOfWork unitOfWork)
    {
            _unitOfWork = unitOfWork;
          
        
    }

    [HttpPost()]
    public async Task<ActionResult> AddNewBatch(BatchPostViewModel model){

        if(await _unitOfWork.BatchRepository.Add(model))
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
