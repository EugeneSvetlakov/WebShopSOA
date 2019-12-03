using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Domain.DTO.Order;

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
                var createOrderModel = new CreateOrderModel
                {
                    OrderViewModel = model,
                    OrderItems = _cartService.TransformCart().Items
                    .Select(i => new OrderItemDTO
                    {
                        Id = i.Key.Id,
                        Quantity = i.Value,
                        Price = i.Key.Price
                    }).ToList()
                };
                var orderResult = _orderService.CreateOrder(
                    createOrderModel, 
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

        #region Api

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartApi(int id)
        {
            _cartService.AddToCart(id);
            return Json(new
            {
                id,
                message = $"Товар id:{id} успешно добавленв корзину"
            });
        }

        public IActionResult DecrementFromCartApi(int id)
        {
            _cartService.DecrementFromCart(id);

            return Json(new
            {
                id,
                message = $"Количество товара id:{id} уменьшено на 1."
            });
        }

        public IActionResult RemoveFromCartApi(int id)
        {
            _cartService.RemoveFromCart(id);

            return Json(new
            {
                id,
                message = $"Товар id:{id} удален из корзины."
            });
        }
        public IActionResult RemoveAllApi()
        {
            _cartService.RemoveAll();

            return Json(new
            {
                message = $"Очистка корзины выполнена."
            });
        }

        #endregion
    }
}