using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Infrastructure.Interfaces;
using WebShopSOA.Infrastructure.Services;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult CartDetails()
        {
            var model = new OrderDetailsViewModel {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);

            return RedirectToAction("CartDetails");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);

            return RedirectToAction("CartDetails");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();

            return RedirectToAction("CartDetails");
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);

            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderResult = _orderService.CreateOrder(
                    model, 
                    _cartService.TransformCart(), 
                    User.Identity.Name);

                _cartService.RemoveAll();

                return RedirectToAction("OrderConfimed", new { id = orderResult.Id });
            }

            var detailsModel = new OrderDetailsViewModel
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };

            return View("CartDetails", detailsModel);
        }

        public IActionResult OrderConfimed(int id)
        {
            ViewData["Id"] = id;
            return View();
        }
    }
}