using WebShopSOA.Domain.Entities.Base;

namespace WebShopSOA.Domain.Entities
{
    public class OrderItem: BaseEntity
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; } // связь в БД через внешний ключ

        public virtual Product Product { get; set; } // связь в БД через внешний ключ
    }
}
