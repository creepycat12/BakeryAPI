using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bakery.api.ViewModels;
using bakery.api.ViewModels.Orders;

namespace bakery.api.Interfaces;

public interface IOrderRepository
{
    public Task<IList<OrdersViewModel>> List();
    public Task <OrdersViewModel> Find(string orderNumber);
    public Task<IList<OrdersViewModel>> Find(DateTime orderDate);
    public Task<bool> Add(OrdersPostViewModel model);
}
