using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Services.ShopProduct
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly ICartStore _CartStore;

        private Cart _Cart { get => _CartStore.Cart; set => _CartStore.Cart = value; }

        public CartService(IProductService productService, ICartStore CartStore)
        {
            _productService = productService;
            _CartStore = CartStore;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
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
            _Cart.Items.Clear();
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
