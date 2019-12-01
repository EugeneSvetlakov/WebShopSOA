using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;
using Microsoft.Extensions.Configuration;

namespace WebShopSOA.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IConfiguration _configuration;

        public IProductService _productService { get; }

        public CatalogController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public IActionResult Products(int? categoryId, int? brandId, int Page = 1)
        {
            var page_size = int.Parse(_configuration["PageSize"]);

            // получаем список отфильтрованных продуктов
            var products = _productService.GetProducts(
                new Domain.Filters.ProductFilter
                {
                    BrandId = brandId,
                    CategoryId = categoryId,
                    Page = Page,
                    PageSize = page_size
                });

            // конвертируем в  CatalogViewModel
            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandName = p.Brand?.Name ?? string.Empty
                }).OrderBy(p => p.Order).ToList(),
                PageViewModel = new PageViewModel
                {
                    PageSize = page_size,
                    PageNumber = Page,
                    TotalItems = products.TotalCount
                }
            };

            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                BrandName = product.Brand?.Name ?? string.Empty,
                Order = product.Order
            });
        }
    }
}