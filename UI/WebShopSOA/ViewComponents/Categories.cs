using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;

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

        public async Task<IViewComponentResult> InvokeAsync(string CategoryId)
        {
            var category_id = int.TryParse(CategoryId, out var id) ? id : (int?)null;

            var Categories = GetCategories(category_id, out var parent_category_id);

            return View(new CategoryCompliteViewModel
            {
                Categories = Categories,
                CurrentCategoryId = category_id,
                CurrentParentCategory = parent_category_id
            });
        }

        private List<CategoryViewModel> GetCategories(int? CategoryId, out int? ParentCategoryId)
        {
            ParentCategoryId = null;

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
                    if (childCategory.Id == CategoryId)
                        ParentCategoryId = CategoryViewModel.Id;

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
