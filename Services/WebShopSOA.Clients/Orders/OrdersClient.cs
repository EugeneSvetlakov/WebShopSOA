using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebShopSOA.Clients.Base;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _Logger;

        public OrdersClient(IConfiguration config, ILogger<OrdersClient> Logger)
            : base(config, $"api/orders")
        {
            _Logger = Logger;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            _Logger.LogInformation($"Client: Запрос списка заказов пользователя {UserName}");
            return Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");
        }

        public OrderDTO GetOrderById(int id) =>
            Get<OrderDTO>($"{_ServiceAddress}/{id}");

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            _Logger.LogInformation($"Client: Созадем новый заказ для пользователя {UserName}");

            return Post($"{_ServiceAddress}/{UserName}", OrderModel)
            .Content
            .ReadAsAsync<OrderDTO>()
            .Result;
        }
    }
}
