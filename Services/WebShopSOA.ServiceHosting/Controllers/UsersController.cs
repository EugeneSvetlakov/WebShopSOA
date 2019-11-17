using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;

namespace WebShopSOA.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User> _UserStore;

        public UsersController(WebShopSOADbContext db)
        {
            _UserStore = new UserStore<User>(db) { AutoSaveChanges = true };
        }
    }
}