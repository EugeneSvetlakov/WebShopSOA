using System;
using System.Collections.Generic;
using WebShopSOA.Domain.Entities.Base;

namespace WebShopSOA.Domain.DTO.Order
{
    public class OrderDTO : NamedEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
