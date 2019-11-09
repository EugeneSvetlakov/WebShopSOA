using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WebShopSOA.Domain.Entities.Base.Interfaces;

namespace WebShopSOA.Domain.Entities.Base
{
    public class NamedEntity : INamedEntity
    {
        [DisplayName("Ид")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }
    }
}
