using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebShopSOA.Clients.Base;
using WebShopSOA.Domain.ViewModels;
using WebShopSOA.Interfaces.Services;

namespace WebShopSOA.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeeService
    {

        public EmployeesClient(IConfiguration config) : base(config, "api/employees") { }

        public IEnumerable<EmployeeView> GetAll() => Get<List<EmployeeView>>(_ServiceAddress);

        public EmployeeView GetById(int id) => Get<EmployeeView>($"{_ServiceAddress}/{id}");

        public void AddNew(EmployeeView model) => Post(_ServiceAddress, model);

        public EmployeeView Update(int id, EmployeeView employee) => Put<EmployeeView>($"{_ServiceAddress}/{id}", employee)
            .Content.ReadAsAsync<EmployeeView>()
            .Result;

        public new void Delete(int id) => Delete($"{_ServiceAddress}/{id}");

        public void Commit() { }
    }
}
