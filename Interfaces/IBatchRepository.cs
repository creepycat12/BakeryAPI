using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels;

namespace bakery.api.Interfaces;

public interface IBatchRepository
{
    public Task<bool> Add(BatchPostViewModel model);
}
