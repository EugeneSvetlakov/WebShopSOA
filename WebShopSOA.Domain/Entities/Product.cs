using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShopSOA.Domain.Entities.Base;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.Entities
{
    [Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        [DisplayName("Порядок")]
        public int Order { get; set; }

        public int CategoryId { get; set; }

        public int? BrandId { get; set; }

        [DisplayName("Изображение")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")]
        [DisplayName("Категория")]
        public virtual Category Category { get; set; }

        [ForeignKey("BrandId")]
        [DisplayName("Брэнд")]
        public virtual Brand Brand { get; set; }
    }
}
