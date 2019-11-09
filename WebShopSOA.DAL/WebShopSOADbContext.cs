using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.DAL
{
    // Добавить миграцию: 
    // 1) -Project WebShopSOA.DAL - проект содержащий DbContext
    // 2) -Name Initial - имя миграции
    // Add-Migration -Project WebShopSOA.DAL -Name Initial
    // Обновить БД:
    // Update-Database
    public class WebShopSOADbContext : IdentityDbContext<User>
    {
        public WebShopSOADbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
