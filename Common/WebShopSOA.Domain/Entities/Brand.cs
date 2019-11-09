using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShopSOA.Domain.Entities.Base;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.Entities
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
