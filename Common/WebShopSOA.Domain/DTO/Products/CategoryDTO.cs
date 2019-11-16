using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities.Base;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    public class CategoryDTO : NamedEntity, IOrderedEntity
    {
        public int? ParentId { get; set; }

        public int Order { get; set; }
    }
}
