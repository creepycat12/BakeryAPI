using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.Interfaces;
using bakery.api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Repositories;

public class BatchRepository : IBatchRepository
{
        private readonly DataContext _context;
    public BatchRepository(DataContext context)
    {
            _context = context;
        
    }
    public async Task<bool> Add(BatchPostViewModel model)
    {
        try
        {
            if( await _context.Products.SingleOrDefaultAsync(c => c.Id == model.ProductId) is null)
        {
            throw new Exception ($"Could not find product with ID {model.ProductId}");
        }
        
        var newBatch = new ProductBatch{
            ProductId = model.ProductId,
            PreperationDate = model.PreparationDate,
            ExpiryDate = model.ExpiryDate
        };


        await _context.ProductBatches.AddAsync(newBatch);
        await _context.SaveChangesAsync();
        
        return true;

        }
        catch (Exception ex)
        {
            throw new Exception (ex.Message);
        }

        

    }
}
