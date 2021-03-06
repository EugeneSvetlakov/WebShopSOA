﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebShopSOA.Clients.Base;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Clients.Products
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration config) : base(config, "api/products") { }

        public PagedProductDTO GetProducts(ProductFilter filter) =>
            Post(_ServiceAddress, filter)
            .Content
            .ReadAsAsync<PagedProductDTO>()
            .Result;

        public ProductDTO GetProductById(int id) =>
            Get<ProductDTO>($"{_ServiceAddress}/{id}");

        public void EditProduct(int id, ProductDTO product) =>
            Post($"{_ServiceAddress}/{id}", product);

        public void DeleteProduct(int id) =>
            Delete($"{_ServiceAddress}/{id}");

        public IEnumerable<BrandDTO> GetBrands() =>
            Get<List<BrandDTO>>($"{_ServiceAddress}/brands");

        public IEnumerable<CategoryDTO> GetCategories() =>
            Get<List<CategoryDTO>>($"{_ServiceAddress}/categories");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{_ServiceAddress}/brands/{id}");

        public CategoryDTO GetCategoryById(int id) => Get<CategoryDTO>($"{_ServiceAddress}/categories/{id}");
    }
}
