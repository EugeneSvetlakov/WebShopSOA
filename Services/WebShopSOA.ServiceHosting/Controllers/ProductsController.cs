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
    /// <summary>
    /// Контроллер товаров
    /// </summary>
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

        /// <summary>
        /// Получить список продуктов
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Отфильтрованный список продуктов из БД</returns>
        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter) =>
            _ProductService.GetProducts(filter);

        /// <summary>
        /// Получить продукт по id
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <returns>Продукт из БД по указанному id</returns>
        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id) => 
            _ProductService.GetProductById(id);

        /// <summary>
        /// Редактировать продукт
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <param name="product">Данные для обновления</param>
        [HttpPut("{id}"), ActionName("Put")]
        public void EditProduct(int id, [FromBody] ProductDTO product) => 
            _ProductService.EditProduct(id, product);

        /// <summary>
        /// Удаленить продукт
        /// </summary>
        /// <param name="id">Id удаляемого продукта</param>
        [HttpDelete("{id}")]
        public void DeleteProduct(int id) => 
            _ProductService.DeleteProduct(id);

        /// <summary>
        /// Получить Брэнды
        /// </summary>
        /// <returns>Список всех Брэндов</returns>
        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => 
            _ProductService.GetBrands();

        /// <summary>
        /// Получить Категории
        /// </summary>
        /// <returns>Список всех категорий из БД</returns>
        [HttpGet("categories")]
        public IEnumerable<CategoryDTO> GetCategories() => _ProductService.GetCategories();
    }
}