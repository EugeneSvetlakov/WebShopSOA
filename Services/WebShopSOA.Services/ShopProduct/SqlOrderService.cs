using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Services.ShopProduct
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebShopSOADbContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebShopSOADbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOrders(string UserName)
        {
            return _context.Orders
                .Include(o=> o.User)
                .Include(o=> o.OrderItems)
                .Where(o => o.User.UserName == UserName)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o=> o.User)
                .Include(o=> o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string UserName)
        {
            var user = _userManager
                .FindByNameAsync(UserName)
                .Result;

            using (var trans = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Name = orderViewModel.Name,
                    Address = orderViewModel.Address,
                    Date = DateTime.Now,
                    Phone = orderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in cartViewModel.Items)
                {
                    var productVm = item.Key;

                    var product = _context.Products.FirstOrDefault(p => p.Id == productVm.Id);

                    if (product == null)
                        throw new InvalidOperationException("Товар не найден в БД");

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Product = product,
                        Price = product.Price,
                        Quantity = item.Value
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                trans.Commit();

                return order;
            }
        }
    }
}
