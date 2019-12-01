using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Services.Map;

namespace WebShopSOA.ViewComponents
{
    //[ViewComponent(Name = "Categories")]
    public class Brands : ViewComponent
    {
        private readonly IProductService _productService;

        public Brands(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string BrandId)
        {
            var Brands = GetBrands();
            return View(new BrandCompliteViewModel
            {
                Brands = Brands,
                CurrentBrandId = int.TryParse(BrandId, out var id) ? id : (int?) null
            });
        }

        private List<BrandViewModel> GetBrands()
        {
            // Получаем список Брэндов
            var brands = _productService.GetBrands().Select(BrandMapper.FromDTO);

            // Заполняем BrandViewModels
            var BrandViewModels = new List<BrandViewModel>();
            foreach (var brand in brands)
            {
                BrandViewModels.Add(new BrandViewModel()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order,
                    ProductCount =
                                _productService
                                .GetProducts(new ProductFilter { BrandId = brand.Id }).Products?
                                .Count() ?? null
                });
            }

            BrandViewModels = BrandViewModels.OrderBy(c => c.Order).ToList();

            return BrandViewModels;
        }
    }
}
