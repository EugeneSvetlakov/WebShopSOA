using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Domain.ViewModels;
using Microsoft.Extensions.Logging;

namespace WebShopSOA.Services.ShopProduct
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<EmployeeView> _employees;
        private readonly ILogger<EmployeeService> _Logger;

        public EmployeeService(ILogger<EmployeeService> Logger)
        {
            _Logger = Logger;

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
            if (model is null) throw new ArgumentNullException(nameof(model));

            if (_employees.Contains(model) || _employees.Any(e => e.Id == model.Id))
            {
                _Logger.LogWarning($"Попытка добавить сотрудника с id:{model.Id}, который уже существует.");
                return;
            }

            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
            _Logger.LogInformation($"Добавлен новый сотрудник с id:{model.Id}");
        }

        public EmployeeView Update(int id, EmployeeView employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var employeeDb = GetById(id);

            if (employeeDb == null)
                throw new InvalidOperationException($"Сотрудник с id:{id} не найден.");

            employeeDb.FirstName = employee.FirstName;
            employeeDb.LastName = employee.LastName;
            employeeDb.Patronymic = employee.Patronymic;
            employeeDb.Age = employee.Age;

            return employeeDb;
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
