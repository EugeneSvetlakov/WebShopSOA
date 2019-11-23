using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities.Base;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    /// <summary>
    /// Категория товара
    /// </summary>
    public class CategoryDTO : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Порядок (для сортировки)
        /// </summary>
        public int Order { get; set; }
    }
}
