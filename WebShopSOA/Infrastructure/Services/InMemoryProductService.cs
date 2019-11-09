using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Infrastructure.Interfaces;

namespace WebShopSOA.Infrastructure.Services
{
    public class InMemoryProductService : IProductService
    {
        private readonly List<Category> _categories;
        private readonly List<Brand> _brands;
        private readonly List<Product> _products;

        public InMemoryProductService()
        {
            // Список категорий
            _categories = new List<Category>
            {
                new Category
                {
                    Id = 0,
                    Name = "sportswear",
                    Order = 0,
                    ParentId = null
                },
                new Category
                {
                    Id = 1,
                    Name = "nike",
                    Order = 0,
                    ParentId = 0
                },
                new Category
                {
                    Id = 2,
                    Name = "under armour",
                    Order = 1,
                    ParentId = 0
                },
                new Category
                {
                    Id = 3,
                    Name = "adidas",
                    Order = 2,
                    ParentId = 0
                },
                new Category
                {
                    Id = 4,
                    Name = "puma",
                    Order = 3,
                    ParentId = 0
                },
                new Category
                {
                    Id = 5,
                    Name = "asics",
                    Order = 4,
                    ParentId = 0
                },
                new Category
                {
                    Id = 6,
                    Name = "mens",
                    Order = 1,
                    ParentId = null
                },
                new Category
                {
                    Id = 7,
                    Name = "fendi",
                    Order = 0,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 8,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 9,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 10,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 11,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 12,
                    Name = "Armani",
                    Order = 5,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 13,
                    Name = "Prada",
                    Order = 6,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 14,
                    Name = "Dolce and Gabbana",
                    Order = 7,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 15,
                    Name = "Chanel",
                    Order = 8,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 16,
                    Name = "Gucci",
                    Order = 1,
                    ParentId = 6
                },
                new Category()
                {
                    Id = 17,
                    Name = "Womens",
                    Order = 2,
                    ParentId = null
                },
                new Category()
                {
                    Id = 18,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 17
                },
                new Category()
                {
                    Id = 19,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 17
                },
                new Category()
                {
                    Id = 20,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 17
                },
                new Category()
                {
                    Id = 21,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 17
                },
                new Category()
                {
                    Id = 22,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 17
                },
                new Category()
                {
                    Id = 23,
                    Name = "Kids",
                    Order = 3,
                    ParentId = null
                },
                new Category()
                {
                    Id = 24,
                    Name = "Fashion",
                    Order = 4,
                    ParentId = null
                },
                new Category()
                {
                    Id = 25,
                    Name = "Households",
                    Order = 5,
                    ParentId = null
                },
                new Category()
                {
                    Id = 26,
                    Name = "Interiors",
                    Order = 6,
                    ParentId = null
                },
                new Category()
                {
                    Id = 27,
                    Name = "Clothing",
                    Order = 7,
                    ParentId = null
                },
                new Category()
                {
                    Id = 28,
                    Name = "Bags",
                    Order = 8,
                    ParentId = null
                },
                new Category()
                {
                    Id = 29,
                    Name = "Shoes225",
                    Order = 9,
                    ParentId = null
                }
            };

            // Список Брэндов
            _brands = new List<Brand>
            {
                new Brand()
                {
                    Id = 0,
                    Name = "Acne",
                    Order = 0
                },
                new Brand()
                {
                    Id = 1,
                    Name = "Grüne Erde",
                    Order = 1
                },
                new Brand()
                {
                    Id = 2,
                    Name = "Albiro2",
                    Order = 2
                },
                new Brand()
                {
                    Id = 3,
                    Name = "Ronhill",
                    Order = 3
                },
                new Brand()
                {
                    Id = 4,
                    Name = "Oddmolly",
                    Order = 4
                },
                new Brand()
                {
                    Id = 5,
                    Name = "Boudestijn",
                    Order = 5
                },
                new Brand()
                {
                    Id = 6,
                    Name = "Rösch creative culture",
                    Order = 6
                }
            };

            // Список товаров
            _products = new List<Product>()
            {
                new Product()
                {
                    Id = 0,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product1.jpg",
                    Order = 0,
                    CategoryId = 1,
                    BrandId = 0
                },
                new Product()
                {
                    Id = 1,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product2.jpg",
                    Order = 1,
                    CategoryId = 1,
                    BrandId = 0
                },
                new Product()
                {
                    Id = 2,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product3.jpg",
                    Order = 2,
                    CategoryId = 1,
                    BrandId = 0
                },
                new Product()
                {
                    Id = 3,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product4.jpg",
                    Order = 3,
                    CategoryId = 1,
                    BrandId = 0
                },
                new Product()
                {
                    Id = 4,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product5.jpg",
                    Order = 4,
                    CategoryId = 1,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 5,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product6.jpg",
                    Order = 5,
                    CategoryId = 1,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 6,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product7.jpg",
                    Order = 6,
                    CategoryId = 1,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 7,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product8.jpg",
                    Order = 7,
                    CategoryId = 24,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 8,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product9.jpg",
                    Order = 8,
                    CategoryId = 24,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 9,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product10.jpg",
                    Order = 9,
                    CategoryId = 24,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 10,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product11.jpg",
                    Order = 10,
                    CategoryId = 24,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 11,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product12.jpg",
                    Order = 11,
                    CategoryId = 24,
                    BrandId = 2
                },
            };
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }

        /// <summary>
        /// Получение всех Брэндов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Brand> GetBrands()
        {
            return _brands;
        }

        /// <summary>
        /// Получение всех товаров
        /// </summary>
        /// <param name="filter">Фильтр товаров</param>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var products = _products;
            if (filter.CategoryId.HasValue)
            {
                products = products.Where(p =>
                p.CategoryId.Equals(filter.CategoryId)).ToList();
            }
            if (filter.BrandId.HasValue)
            {
                products = products.Where(p =>
                        p.BrandId.HasValue && p.BrandId.Value.Equals(filter.BrandId.Value))
                    .ToList();
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void EditProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
