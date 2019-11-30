using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _CartService;

        public CartViewComponent(ICartService CartService)
        {
            _CartService = CartService;
        }

        public IViewComponentResult Invoke() => View(_CartService.TransformCart());
    }
}
