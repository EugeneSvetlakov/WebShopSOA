using System;
using System.Collections.Generic;
using System.Text;

namespace WebShopSOA.Domain.Entities.Base.Interfaces
{
    public interface IOrderedEntity
    {
        /// <summary>
        /// Порядок
        /// </summary>
        int Order { get; set; }
    }
}
