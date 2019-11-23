using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    /// <summary>
    /// Брэнд товара
    /// </summary>
    public class BrandDTO : INamedEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
