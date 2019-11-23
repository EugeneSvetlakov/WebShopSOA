using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string UserName);

        OrderDTO GetOrderById(int id);

        OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName);
    }
}
