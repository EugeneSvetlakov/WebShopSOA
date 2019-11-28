using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebShopSOA.Domain.Filters;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Controllers
{
    public class SitemapController : Controller
    {
        public IActionResult Index([FromServices] IProductService ProductService)
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Contacts", "Home")),
                new SitemapNode(Url.Action("BlogList", "Blog")),
                new SitemapNode(Url.Action("BlogSingl", "Blog")),
                new SitemapNode(Url.Action("Products", "Catalog")),
                new SitemapNode(Url.Action("ProductDetails", "Catalog")),
                new SitemapNode(Url.Action("Index", "WebApiTest"))
            };

            foreach (var category in ProductService.GetCategories()) 
                nodes.Add(new SitemapNode(Url.Action("Products", "Catalog", new { CategoryId = category.Id })));

            foreach (var brand in ProductService.GetBrands()) 
                nodes.Add(new SitemapNode(Url.Action("Products", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductService.GetProducts(new ProductFilter()))
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "Catalog", new { ProductId = product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}