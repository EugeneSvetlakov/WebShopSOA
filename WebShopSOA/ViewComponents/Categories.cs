using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Infrastructure.Interfaces;
using WebShopSOA.ViewModels;

namespace WebShopSOA.ViewComponents
{
    //[ViewComponent(Name = "Categories")]
    public class Categories : ViewComponent
    {
        private readonly IProductService _productService;

        public Categories(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categories = GetCategories();
            return View(Categories);
        }

        private List<CategoryViewModel> GetCategories()
        {
            var categories = _productService.GetCategories();
            // получим и заполним родительские категории
            var rootCategories = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var CategoryViewModels = new List<CategoryViewModel>();
            foreach (var parentCategory in rootCategories)
            {
                CategoryViewModels.Add(new CategoryViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                });
            }
            // получим и заполним дочерние категории
            foreach (var CategoryViewModel in CategoryViewModels)
            {
                var childCategories = categories.Where(c => c.ParentId == CategoryViewModel.Id);
                foreach (var childCategory in childCategories)
                {
                    CategoryViewModel.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = CategoryViewModel
                    });
                }
                CategoryViewModel.ChildCategories = CategoryViewModel.ChildCategories.OrderBy(c => c.Order).ToList();
            }
            CategoryViewModels = CategoryViewModels.OrderBy(c => c.Order).ToList();

            return CategoryViewModels;
        }
    }
}
