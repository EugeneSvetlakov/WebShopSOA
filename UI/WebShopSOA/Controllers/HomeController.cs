using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Infrastructure;

namespace WebShopSOA.Controllers
{
    public class HomeController : Controller
    {
        [SimpleActionFilter]
        public IActionResult Index()
        {
            //return StatusCode(403);
            //return new RedirectResult("https://google.com");
            //return new RedirectToActionResult("Blog", "Home", null);
            //return new JsonResult("{'key': 'value'}");
            //return NotFound(); //return NotFoundResult();
            //return new ContentResult();

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult ErrorStatus(string id)
        {
            switch (id)
            {
                default:
                    return Content($"Статусный код {id}");
                case "404":
                    return RedirectToAction(nameof(PageNotFound));
            }
            return NoContent();
        }
    }
}