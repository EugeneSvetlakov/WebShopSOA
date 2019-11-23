using WebShopSOA.Domain.DTO.Order;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.Services.Map
{
    public static class OrderItemMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem orderItem) =>
            orderItem is null ? null : new OrderItemDTO
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity
            };

        public static OrderItem FromDTO(this OrderItemDTO order) =>
            order is null ? null : new OrderItem
            {
                Id = order.Id,
                Price = order.Price,
                Quantity = order.Quantity
            };
    }
}
