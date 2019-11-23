using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Services.ShopProduct
{
    public class SqlProductService : IProductService
    {
        private readonly WebShopSOADbContext _context;

        public SqlProductService(WebShopSOADbContext context)
        {
            this._context = context;
        }


        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Category) // "жадная загрузка" - механизм EF
                .Include(p=> p.Brand) // "жадная загрузка" - механизм EF
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
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

            return query.ToList();
        }

        public void EditProduct(Product product)
        {
            var DbProduct = _context.Products.First(p => p.Id == product.Id);

            if(product.Id == DbProduct.Id)
            {
                DbProduct.Name = product.Name;
                DbProduct.Price = product.Price;
            }
            else
            {
                _context.Products.Add(product);
            }

            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

    }
}
