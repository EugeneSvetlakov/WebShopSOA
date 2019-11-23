using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger<OrdersController> _Logger;

        public OrdersController(IOrderService OrderService, ILogger<OrdersController> Logger)
        {
            _OrderService = OrderService;
            _Logger = Logger;
        }

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            _Logger.LogInformation($"WebApi: Запрос списка заказов пользователя {UserName}");
            return _OrderService.GetUserOrders(UserName);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id) =>
            _OrderService.GetOrderById(id);

        [HttpPost("{UserName?}")]
        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            _Logger.LogInformation($"WebApi: Создание нового заказа для пользователя {UserName}");

            return _OrderService.CreateOrder(OrderModel, UserName);
        }
    }
}