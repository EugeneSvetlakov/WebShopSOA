using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.Services.Map
{
    public static class ProductMapper
    {
        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Price = product.Price,
            Order = product.Order,
            Brand = product.Brand.ToDTO(),
            BrandId = product.Brand?.Id,
            Category = product.Category.ToDTO(),
            CategoryId = product.Category.Id
        };


        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Price = product.Price,
            Order = product.Order,
            Brand = product.Brand.FromDTO(),
            BrandId = product.Brand?.Id,
            Category = product.Category.FromDTO(),
            CategoryId = product.Category.Id
        };
    }

    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand brand) => brand is null ? null : new BrandDTO
        {
            Id = brand.Id,
            Name = brand.Name
        };

        public static Brand FromDTO(this BrandDTO brand) => brand is null ? null : new Brand
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(this Category category) => category is null ? null : new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Order = category.Order,
            ParentId = category.ParentId
        };

        public static Category FromDTO(this CategoryDTO category) => category is null ? null : new Category
        {
            Id = category.Id,
            Name = category.Name,
            Order = category.Order,
            ParentId = category.ParentId
        };
    }
}
