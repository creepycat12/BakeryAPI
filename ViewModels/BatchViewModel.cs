using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bakery.api.ViewModels;

public class BatchViewModel
{
    public DateTime PreperationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
