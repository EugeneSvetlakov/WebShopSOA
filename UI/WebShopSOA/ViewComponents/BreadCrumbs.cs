using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.ViewModels.BreadCrumbs;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.ViewComponents
{
    public class BreadCrumbs : ViewComponent
    {
        private readonly IProductService _ProductService;

        public BreadCrumbs(IProductService ProductService) => _ProductService = ProductService;

        public IViewComponentResult Invoke(BreadCrumbType Type, int id, BreadCrumbType FromType)
        {
            switch (Type)
            {
                default: return View(Array.Empty<BreadCrumbViewModel>());

                case BreadCrumbType.Category:
                    return View(new [] 
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbType = Type,
                            Id = id.ToString(),
                            Name = _ProductService.GetCategoryById(id).Name
                        }
                    });
                case BreadCrumbType.Brand:
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbType = Type,
                            Id = id.ToString(),
                            Name = _ProductService.GetBrandById(id).Name
                        }
                    });
                case BreadCrumbType.Product:
                    return View(GetProductBreadCrumbs(_ProductService.GetProductById(id), FromType));
            }

        }

        private static IEnumerable<BreadCrumbViewModel> GetProductBreadCrumbs(ProductDTO Product, BreadCrumbType FromType) =>
            new[]
            {
                new BreadCrumbViewModel
                {
                    BreadCrumbType = FromType,
                    Id = FromType == BreadCrumbType.Category
                        ? Product.CategoryId.ToString()
                        : Product.BrandId.ToString(),
                    Name = FromType == BreadCrumbType.Category
                        ? Product.Category.Name
                        : Product.Brand.Name
                },
                new BreadCrumbViewModel
                {
                    BreadCrumbType = BreadCrumbType.Product,
                    Id = Product.Id.ToString(),
                    Name = Product.Name
                }
            };
    }
}
