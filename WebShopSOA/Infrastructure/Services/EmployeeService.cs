using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Infrastructure.Interfaces;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<EmployeeView> _employees;

        public EmployeeService()
        {
            _employees = new List<EmployeeView>
            {
                new EmployeeView
                {
                    Id = 1,
                    LastName = "Иванов",
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    Age = 35
                },
                new EmployeeView
                {
                    Id = 2,
                    LastName = "Петров",
                    FirstName = "Ким",
                    Patronymic = "Торжиевич",
                    Age = 22
                }
            };
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddNew(EmployeeView model)
        {
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }

        public void Commit()
        {
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp == null)
                return;

            _employees.Remove(emp);
        }

    }
}
