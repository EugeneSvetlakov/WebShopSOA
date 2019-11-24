using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Services.ShopProduct;
using Assert = Xunit.Assert;

namespace WebShopSOA.Services.Tests.ShopProduct
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void Cart_Class_ItemsCount_Return_Correct_Quantity()
        {
            const int expected_count = 4;

            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem{ProductId = 1, Quantity = 1},
                    new CartItem{ProductId = 2, Quantity = 3}
                }
            };

            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Return_Correct_ItemsCount()
        {
            const int expected_count = 4;

            var cart_view_model = new CartViewModel
            {
                Discount = 0,
                Items = new Dictionary<ProductViewModel, int>
                {
                    {new ProductViewModel{ Id = 1, Name = "Prodect 1", Price = 1.05m}, 1 },
                    {new ProductViewModel{ Id = 2, Name = "Prodect 2", Price = 0.15m}, 3 }
                }
            };

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartService_AddToCart_AddNewItem_WorkCorrect()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>()
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            const int expected_product_id = 5;

            cart_service.AddToCart(expected_product_id);

            Assert.Equal(1, cart.ItemsCount);
            Assert.Single(cart.Items); // == Assert.Equal(1, cart.Items.Count);
            Assert.Equal(expected_product_id, cart.Items[0].ProductId);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_Remove_Correct_Item()
        {
            const int removing_item_id = 1;
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem{ProductId = removing_item_id, Quantity = 2},
                    new CartItem{ProductId = 3, Quantity = 3}
                }
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            cart_service.RemoveFromCart(removing_item_id);

            Assert.Single(cart.Items);
            Assert.Equal(3, cart.Items[0].ProductId);
        }

        [TestMethod]
        public void CartService_RemoveAll_ClearCart()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem{ProductId = 1, Quantity = 2},
                    new CartItem{ProductId = 3, Quantity = 3}
                }
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            cart_service.RemoveAll();

            Assert.Empty(cart.Items);
        }

        [TestMethod]
        public void CartService_DecrementFromCart_Correct()
        {
            const int decrementing_item_id = 1;
            const int decrementing_item_quantity = 2;
            var decrementing_item = new CartItem
            {
                ProductId = decrementing_item_id,
                Quantity = decrementing_item_quantity
            };

            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    decrementing_item,
                    new CartItem{ProductId = 3, Quantity = 3}
                }
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            cart_service.DecrementFromCart(decrementing_item_id);

            Assert.Equal(4, cart.ItemsCount);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(decrementing_item_id, cart.Items[0].ProductId);
            Assert.Equal(1, cart.Items.FirstOrDefault(i => i.ProductId == decrementing_item_id).Quantity);
            Assert.Equal(3, cart.Items[1].Quantity);
        }

        [TestMethod]
        public void CartService_DecrementFromCart_WhenItemQuantity_0_Remove_This_Item()
        {
            const int decrementing_item_id = 1;
            const int decrementing_item_quantity = 1;
            var decrementing_item = new CartItem
            {
                ProductId = decrementing_item_id,
                Quantity = decrementing_item_quantity
            };

            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    decrementing_item,
                    new CartItem{ProductId = 3, Quantity = 3}
                }
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            cart_service.DecrementFromCart(decrementing_item_id);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Single(cart.Items);
            Assert.Null(cart.Items.FirstOrDefault(i => i.ProductId == decrementing_item_id));
            Assert.Equal(3, cart.Items.Last().Quantity);
        }

        [TestMethod]
        public void CartService_AddToCart_IncreaseQuantity_Correct()
        {
            const int incrementing_item_id = 1;
            const int incrementing_item_quantity = 2;
            var decrementing_item = new CartItem
            {
                ProductId = incrementing_item_id,
                Quantity = incrementing_item_quantity
            };

            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    decrementing_item,
                    new CartItem{ProductId = 3, Quantity = 4}
                }
            };

            var product_service_mock = new Mock<IProductService>();

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            cart_service.AddToCart(incrementing_item_id);

            Assert.Equal(7, cart.ItemsCount);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(incrementing_item_id, cart.Items[0].ProductId);
            Assert.Equal(3, cart.Items.FirstOrDefault(i => i.ProductId == incrementing_item_id).Quantity);
            Assert.Equal(4, cart.Items[1].Quantity);
        }

        [TestMethod]
        public void CartService_TransformCart_WorkCorrect()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem{ProductId = 1, Quantity = 1},
                    new CartItem{ProductId = 3, Quantity = 3}
                }
            };

            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Product Id 1",
                    Order = 0,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand 1"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 1,
                        Name = "Category 1",
                        Order = 0,
                        ParentId = null
                    },
                    ImageUrl = "product_1.png",
                    Price = 1.2m
                },
                new ProductDTO
                {
                    Id = 3,
                    Name = "Product Id 3",
                    Order = 0,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand 2"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 2,
                        Name = "Category 2",
                        Order = 0,
                        ParentId = 1
                    },
                    ImageUrl = "product_3.png",
                    Price = 2.8m
                }
            };

            var product_service_mock = new Mock<IProductService>();
            product_service_mock
                .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(products);

            var cart_store_mock = new Mock<ICartStore>();
            cart_store_mock
                .Setup(c => c.Cart)
                .Returns(cart);

            var cart_service = new CartService(product_service_mock.Object, cart_store_mock.Object);

            var result = cart_service.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.2m, result.Items.First().Key.Price);
        }
    }
}
