using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    public class ProductDTO : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; }

        public BrandDTO Brand { get; set; }

        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
