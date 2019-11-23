﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.Services.Map
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order order) =>
            order is null ? null : new OrderDTO
            {
                Id = order.Id,
                Address = order.Address,
                Name = order.Name,
                Date = order.Date,
                Phone = order.Phone,
                OrderItems = order.OrderItems
                            .Select(OrderItemMapper.ToDTO)
            };

        public static Order FromDTO(this OrderDTO order) =>
            order is null ? null : new Order
            {
                Id = order.Id,
                Address = order.Address,
                Name = order.Name,
                Date = order.Date,
                Phone = order.Phone,
                OrderItems = order.OrderItems
                            .Select(OrderItemMapper.FromDTO).ToList()
            };
    }
}
