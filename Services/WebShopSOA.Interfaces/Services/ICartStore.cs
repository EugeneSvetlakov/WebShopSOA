using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
