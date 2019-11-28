using System;
using System.Collections.Generic;
using System.Text;

namespace WebShopSOA.Domain.ViewModels
{
    public class BrandCompliteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
