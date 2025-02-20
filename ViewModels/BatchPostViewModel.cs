using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class BatchPostViewModel
{
    public int ProductId { get; set; }
    public DateTime PreparationDate { get; set; }
    public DateTime ExpiryDate { get; set; }  
}
