using System;
using System.Collections.Generic;
using System.Text;

namespace WebShopSOA.Domain.ViewModels
{
    public class CategoryCompliteViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int? CurrentParentCategory { get; set; }

        public int? CurrentCategoryId { get; set; }

    }
}
