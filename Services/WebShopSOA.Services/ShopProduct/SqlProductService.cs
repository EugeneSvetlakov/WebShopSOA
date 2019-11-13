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

        public ProductDTO GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category) // "жадная загрузка" - механизм EF
                .Include(p => p.Brand) // "жадная загрузка" - механизм EF
                .FirstOrDefault(p => p.Id == id);
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                Brand = new BrandDTO
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                }
            };
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

            return query.ToList().Select(p => 
            new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Order = p.Order,
                Price = p.Price,
                Brand = new BrandDTO
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name
                }
            });
        }

        public void EditProduct(ProductDTO product)
        {
            var DbProduct = _context.Products.First(p => p.Id == product.Id);

            if(product.Id == DbProduct.Id)
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
