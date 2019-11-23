using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Domain.DTO.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
