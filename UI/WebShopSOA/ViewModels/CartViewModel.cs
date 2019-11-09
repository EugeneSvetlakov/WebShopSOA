using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopSOA.ViewModels
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }

        public int ItemsCount => Items?.Sum(x => x.Value) ?? 0;

        public int Discount { get; set; } = 0;
    }
}
