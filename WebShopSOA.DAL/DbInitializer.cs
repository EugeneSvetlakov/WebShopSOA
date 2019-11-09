using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.DAL
{
    public class DbInitializer
    {
        // Внесение стартовых данных в БД
        public static void Initialize(WebShopSOADbContext context)
        {
            // Создание и Обновление базы до последнего состояния
            context.Database.Migrate();

            // Проверка что база имеет записи
            if (context.Products.Any())
            {
                return; // Завершение "стартового наполнения"
            }

            // Внесение категорий
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "sportswear",
                    Order = 0,
                    ParentId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "nike",
                    Order = 0,
                    ParentId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "under armour",
                    Order = 1,
                    ParentId = 1
                },
                new Category
                {
                    Id = 4,
                    Name = "adidas",
                    Order = 2,
                    ParentId = 1
                },
                new Category
                {
                    Id = 5,
                    Name = "puma",
                    Order = 3,
                    ParentId = 1
                },
                new Category
                {
                    Id = 6,
                    Name = "asics",
                    Order = 4,
                    ParentId = 1
                },
                new Category
                {
                    Id = 7,
                    Name = "mens",
                    Order = 1,
                    ParentId = null
                },
                new Category
                {
                    Id = 8,
                    Name = "fendi",
                    Order = 0,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 9,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 10,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 11,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 12,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 13,
                    Name = "Armani",
                    Order = 5,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 14,
                    Name = "Prada",
                    Order = 6,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 15,
                    Name = "Dolce and Gabbana",
                    Order = 7,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 16,
                    Name = "Chanel",
                    Order = 8,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 17,
                    Name = "Gucci",
                    Order = 1,
                    ParentId = 7
                },
                new Category()
                {
                    Id = 18,
                    Name = "Womens",
                    Order = 2,
                    ParentId = null
                },
                new Category()
                {
                    Id = 19,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 18
                },
                new Category()
                {
                    Id = 20,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 18
                },
                new Category()
                {
                    Id = 21,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 18
                },
                new Category()
                {
                    Id = 22,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 18
                },
                new Category()
                {
                    Id = 23,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 18
                },
                new Category()
                {
                    Id = 24,
                    Name = "Kids",
                    Order = 3,
                    ParentId = null
                },
                new Category()
                {
                    Id = 25,
                    Name = "Fashion",
                    Order = 4,
                    ParentId = null
                },
                new Category()
                {
                    Id = 26,
                    Name = "Households",
                    Order = 5,
                    ParentId = null
                },
                new Category()
                {
                    Id = 27,
                    Name = "Interiors",
                    Order = 6,
                    ParentId = null
                },
                new Category()
                {
                    Id = 28,
                    Name = "Clothing",
                    Order = 7,
                    ParentId = null
                },
                new Category()
                {
                    Id = 29,
                    Name = "Bags",
                    Order = 8,
                    ParentId = null
                },
                new Category()
                {
                    Id = 30,
                    Name = "Shoes225",
                    Order = 9,
                    ParentId = null
                }
            };
            using (var trans = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Categories] ON");

                foreach (var item in categories)
                {
                    context.Categories.Add(item);
                }

                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Categories] OFF");
                trans.Commit();
            }

            // Внесение брэндов
            var brands = new List<Brand>
            {
                new Brand()
                {
                    Id = 1,
                    Name = "Acne",
                    Order = 0
                },
                new Brand()
                {
                    Id = 2,
                    Name = "Grüne Erde",
                    Order = 1
                },
                new Brand()
                {
                    Id = 3,
                    Name = "Albiro2",
                    Order = 2
                },
                new Brand()
                {
                    Id = 4,
                    Name = "Ronhill",
                    Order = 3
                },
                new Brand()
                {
                    Id = 5,
                    Name = "Oddmolly",
                    Order = 4
                },
                new Brand()
                {
                    Id = 6,
                    Name = "Boudestijn",
                    Order = 5
                },
                new Brand()
                {
                    Id = 7,
                    Name = "Rösch creative culture",
                    Order = 6
                }
            };
            using (var trans = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");

                foreach (var item in brands)
                {
                    context.Brands.Add(item);
                }

                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                trans.Commit();
            }

            // Внесение продуктов
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product1.jpg",
                    Order = 0,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 2,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product2.jpg",
                    Order = 1,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 3,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product3.jpg",
                    Order = 2,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 4,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product4.jpg",
                    Order = 3,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product()
                {
                    Id = 5,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product5.jpg",
                    Order = 4,
                    CategoryId = 2,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 6,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product6.jpg",
                    Order = 5,
                    CategoryId = 2,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 7,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product7.jpg",
                    Order = 6,
                    CategoryId = 2,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 8,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product8.jpg",
                    Order = 7,
                    CategoryId = 25,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 9,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product9.jpg",
                    Order = 8,
                    CategoryId = 25,
                    BrandId = 2
                },
                new Product()
                {
                    Id = 10,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product10.jpg",
                    Order = 9,
                    CategoryId = 25,
                    BrandId = 3
                },
                new Product()
                {
                    Id = 11,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product11.jpg",
                    Order = 10,
                    CategoryId = 25,
                    BrandId = 3
                },
                new Product()
                {
                    Id = 12,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product12.jpg",
                    Order = 11,
                    CategoryId = 25,
                    BrandId = 3
                },
            };
            using (var trans = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");

                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");
                trans.Commit();
            }
        }

        public static void InitializeUsers(IServiceProvider services)
        {
            var roleManager = services.GetService<RoleManager<IdentityRole>>();

            EnsureRole(roleManager, "Users");
            EnsureRole(roleManager, "Administrators");

            EnsureRoleToUser(services, "Admin", "Administrators", "admin123");
        }

        private static void EnsureRoleToUser(IServiceProvider services, string userName, string roleName, string pass)
        {
            var userManager = services.GetService<UserManager<User>>();
            var users = services.GetService<IUserStore<User>>();

            if(users.FindByNameAsync(userName, CancellationToken.None).Result != null)
            {
                return;
            }

            var adminUser = new User
            {
                UserName = userName,
                Email = $"{userName}@domain.com"
            };

            if (userManager.CreateAsync(adminUser, pass).Result.Succeeded) // добавление пользователя в БД
                userManager.AddToRoleAsync(adminUser, roleName).Wait(); // назначение роли
        }

        private static void EnsureRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }
    }
}
