using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase, IProductService
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter) =>
            _ProductService.GetProducts(filter);

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id) => 
            _ProductService.GetProductById(id);

        [HttpPut("{id}"), ActionName("Put")]
        public void EditProduct(int id, [FromBody] ProductDTO product) => 
            _ProductService.EditProduct(id, product);

        [HttpDelete("{id}")]
        public void DeleteProduct(int id) => 
            _ProductService.DeleteProduct(id);

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => 
            _ProductService.GetBrands();

        [HttpGet("categories")]
        public IEnumerable<CategoryDTO> GetCategories() => _ProductService.GetCategories();
    }
}