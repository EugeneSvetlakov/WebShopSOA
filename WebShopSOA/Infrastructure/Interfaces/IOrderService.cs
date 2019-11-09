using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.Entities;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string UserName);

        Order GetOrderById(int id);
        Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string UserName);
    }
}
