using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Infrastructure.Interfaces;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Infrastructure.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart _Cart
        {
            get
            {
                var cookie = _httpContextAccessor
                    .HttpContext
                    .Request
                    .Cookies[_cartName];
                string json = string.Empty;

                Cart cart = null;

                if(cookie == null)
                {
                    cart = new Cart { Items = new List<CartItem>() };
                    json = JsonConvert.SerializeObject(cart);

                    _httpContextAccessor
                        .HttpContext
                        .Response
                        .Cookies
                        .Append(
                        _cartName,
                        json,
                        new CookieOptions()
                        {
                            Expires = DateTime.Now.AddDays(1)
                        });

                    return cart;
                }

                json = cookie;
                cart = JsonConvert.DeserializeObject<Cart>(json);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(
                    _cartName,
                    json,
                    new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                return cart;
            }

            set
            {
                var json = JsonConvert.SerializeObject(value);

                _httpContextAccessor
                    .HttpContext
                    .Response.Cookies
                    .Delete(_cartName);
                _httpContextAccessor
                    .HttpContext
                    .Response.Cookies
                    .Append(
                    _cartName,
                    json,
                    new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
            }
        }

        public CookieCartService(IProductService productService,
            IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

            _cartName = "cart"
                + (_httpContextAccessor
                .HttpContext.User.Identity.IsAuthenticated
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "");
        }

        public void DecrementFromCart(int id)
        {
            var cart = _Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if(item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;

                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }

            _Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                cart.Items.Remove(item);

            _Cart = cart;
        }

        public void RemoveAll()
        {
            _Cart = new Cart { Items = new List<CartItem>() };
        }


        public void AddToCart(int id)
        {
            var cart = _Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });

            _Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter()
            {
                Ids = _Cart.Items.Select(i => i.ProductId).ToList()
            }).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Price = p.Price,
                BrandName = p.Brand != null ? p.Brand.Name : string.Empty,
                Order = p.Order
            }).ToList();

            var cartViewModel = new CartViewModel
            {
                Items = _Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return cartViewModel;
        }
    }
}
