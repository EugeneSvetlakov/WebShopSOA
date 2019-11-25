using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopSOA.Controllers;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebShopSOA.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void ProductDetails_Returns_WithCorrect_Item()
        {
            // A-A-A = Arrange-Action-Assert
            #region Arrange

            const int expected_id = 1;
            const decimal expected_price = 10m;
            var expected_name = $"Item id {expected_id}";
            var expected_brand = $"Brand of item {expected_id}";

            var product_service_mock = new Mock<IProductService>();
            product_service_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Price = 10m,
                    Name = $"Item id {id}",
                    ImageUrl = $"image_id_{id}",
                    Order = 0,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand of item {id}"
                    }
                });

            var controller = new CatalogController(product_service_mock.Object);

            #endregion

            #region Action

            var result = controller.ProductDetails(expected_id);

            #endregion

            #region Assert

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_id, model.Id);
            Assert.Equal(expected_price, model.Price);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_brand, model.BrandName);

            #endregion
        }

        [TestMethod]
        public void ProductDetails_Return_NotFound_IfProduct_NotExist()
        {
            #region Assign
            var product_service_mock = new Mock<IProductService>();

            product_service_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns(default(ProductDTO));

            var controller = new CatalogController(product_service_mock.Object);

            #endregion

            #region Action

            var result = controller.ProductDetails(1);

            #endregion

            #region Assert

            Assert.IsType<NotFoundResult>(result);

            #endregion
        }

        [TestMethod]
        public void Products_Return_Correct_View()
        {
            var product_service_mock = new Mock<IProductService>();

            product_service_mock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new[] 
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Item 1",
                        Price = 10m,
                        ImageUrl = "image_id_1.png",
                        Order = 0,
                        Brand = new BrandDTO
                        {
                            Id = 1,
                            Name = "Brand of item 1"
                        }
                    },
                    new ProductDTO
                    {
                        Id = 2,
                        Name = "Item 2",
                        Price = 12m,
                        ImageUrl = "image_id_2.png",
                        Order = 0,
                        Brand = new BrandDTO
                        {
                            Id = 2,
                            Name = "Brand of item 2"
                        }
                    }
                });

            var controller = new CatalogController(product_service_mock.Object);

            const int expected_category_id = 1;
            const int expected_brand_id = 5;

            var result = controller.Products(expected_category_id, expected_brand_id);

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Assert.Equal(2, model.Products.Count());
            Assert.Equal(expected_brand_id, model.BrandId);
            Assert.Equal(expected_category_id, model.CategoryId);

            Assert.Equal("Brand of item 1", model.Products.First().BrandName);

        }
    }
}
