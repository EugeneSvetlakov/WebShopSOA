using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    /// <summary>
    /// Товар
    /// </summary>
    public class ProductDTO : INamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Проядкок (для сортировки)
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Изображение товара
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор Брэнда (может отсутствовать)
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Объект Брэнд
        /// </summary>
        public BrandDTO Brand { get; set; }

        /// <summary>
        /// Идентификатор Категории
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Объект Категория
        /// </summary>
        public CategoryDTO Category { get; set; }
    }
}
