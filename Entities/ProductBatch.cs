using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class ProductBatch
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public DateTime PreperationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Product Product { get; set; }
}
