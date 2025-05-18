using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal PackPrice { get; set; }
    public decimal Weight_kg { get; set; }
    public int AmountInPack { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public IList<ProductBatch> ProductBatches { get; set; }
    public IList<OrderProduct> OrderProducts{ get; set; }
}
