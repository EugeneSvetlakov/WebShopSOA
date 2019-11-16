using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebShopSOA.Clients.Base;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration config) : base(config, $"api/orders") { }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => 
            Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");

        public OrderDTO GetOrderById(int id) => 
            Get<OrderDTO>($"{_ServiceAddress}/{id}");

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) =>
            Post($"{_ServiceAddress}/{UserName}", OrderModel)
            .Content
            .ReadAsAsync<OrderDTO>()
            .Result;
    }
}
