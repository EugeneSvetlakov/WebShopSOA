﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Services.Map;
using Microsoft.Extensions.Logging;

namespace WebShopSOA.Services.ShopProduct
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebShopSOADbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SqlOrderService> _Logger;

        public SqlOrderService(WebShopSOADbContext context, UserManager<User> userManager, ILogger<SqlOrderService> Logger)
        {
            _context = context;
            _userManager = userManager;
            _Logger = Logger;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _context.Orders
                .Include(o=> o.User)
                .Include(o=> o.OrderItems)
                .Where(o => o.User.UserName == UserName)
                .ToList().
                Select(OrderMapper.ToDTO);
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
            return order.ToDTO();
        }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _userManager
                .FindByNameAsync(UserName)
                .Result;

            using (var trans = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Date = DateTime.Now,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in OrderModel.OrderItems)
                {
                    //var productVm = item.Id;

                    var product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                    if (product == null)
                        throw new InvalidOperationException($"Товар с id:{item.Id} не найден в БД");

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Product = product,
                        Price = product.Price,
                        Quantity = item.Quantity
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                trans.Commit();

                _Logger.LogInformation("Заказ для пользователя {0} создан успешно, номер заказа {1}", user.UserName, order.Id);

                return order.ToDTO();
            }
        }
    }
}
