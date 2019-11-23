using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.DAL;
using WebShopSOA.Domain.DTO.Products;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Services.Map;

namespace WebShopSOA.Services.ShopProduct
{
    public class SqlProductService : IProductService
    {
        private readonly WebShopSOADbContext _context;

        public SqlProductService(WebShopSOADbContext context)
        {
            this._context = context;
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return _context
                .Brands
                .Include(p => p.Products) // "жадная загрузка" Продуктов - механизм EF
                .Select(BrandMapper.ToDTO)
                .ToList();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context
                .Categories
                .Include(p => p.Products) // "жадная загрузка" Продуктов - механизм EF
                .Select(CategoryMapper.ToDTO)
                .ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category) // "жадная загрузка" Категорий - механизм EF
                .Include(p => p.Brand) // "жадная загрузка" Брэндов - механизм EF
                .FirstOrDefault(p => p.Id == id);
            return product.ToDTO();
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include(p=> p.Category)
                .Include(p=> p.Brand)
                .AsQueryable();
            if (filter.BrandId.HasValue)
            {
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            }
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));
            }

            return query.ToList().Select(ProductMapper.ToDTO);
        }

        public void EditProduct(int id, ProductDTO product)
        {
            var DbProduct = _context.Products.First(p => p.Id == product.Id);

            if(id == DbProduct.Id)
            {
                DbProduct.Name = product.Name;
                DbProduct.Price = product.Price;
            }
            else
            {
                _context.Products.Add(new Product
                {
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Order = product.Order,
                    Price = product.Price,
                    Brand = new Brand
                    {
                        Id = product.Brand.Id,
                        Name = product.Brand.Name
                    }
                });
            }

            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

    }
}
