using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.DAL;

namespace WebShopSOA.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<IdentityRole> _RoleStore;

        public RolesController(WebShopSOADbContext db)
        {
            _RoleStore = new RoleStore<IdentityRole>(db) { AutoSaveChanges = true };
        }
    }
}