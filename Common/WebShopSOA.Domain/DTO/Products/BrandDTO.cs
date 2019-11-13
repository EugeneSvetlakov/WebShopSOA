using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.DTO.Products
{
    public class BrandDTO : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
