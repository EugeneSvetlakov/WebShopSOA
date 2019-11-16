using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public OrdersController(IOrderService OrderService) => 
            _OrderService = OrderService;

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => 
            _OrderService.GetUserOrders(UserName);

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id) => 
            _OrderService.GetOrderById(id);

        [HttpPost("{UserName?}")]
        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) =>
            _OrderService.CreateOrder(OrderModel, UserName);
    }
}