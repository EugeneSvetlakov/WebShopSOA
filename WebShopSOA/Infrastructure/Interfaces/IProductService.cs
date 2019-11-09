using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Infrastructure.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Brand> GetBrands();

        IEnumerable<Category> GetCategories();

        IEnumerable<Product> GetProducts(ProductFilter filter);

        // CRUD Product
        // Create/Update
        void EditProduct(Product product);

        // Read
        Product GetProductById(int id);

        // Delete
        void DeleteProduct(int id);
    }
}
