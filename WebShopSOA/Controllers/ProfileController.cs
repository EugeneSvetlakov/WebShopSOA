using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Infrastructure.Interfaces;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrderService _orderService;

        public ProfileController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _orderService.GetUserOrders(User.Identity.Name);

            var orderModels = new List<UserOrderViewModel>(orders.Count());

            foreach (var order in orders)
            {
                orderModels.Add(new UserOrderViewModel()
                {
                    Id = order.Id,
                    Address = order.Address,
                    Name = order.Name,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(o=> o.Price * o.Quantity)
                });
            }

            return View(orderModels);
        }
    }
}