using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase, IEmployeeService
    {
        private readonly IEmployeeService _EmployeeService;

        public EmployeesController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeView> GetAll() => _EmployeeService.GetAll();

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeView GetById(int id) => _EmployeeService.GetById(id);

        [HttpPost, ActionName("Post")]
        public void AddNew(EmployeeView model) => _EmployeeService.AddNew(model);

        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeView Update(int id, [FromBody] EmployeeView employee) => _EmployeeService.Update(id, employee);

        [HttpDelete("{id}")]
        public void Delete(int id) => _EmployeeService.Delete(id);

        [NonAction]
        public void Commit() => _EmployeeService.Commit();
    }
}