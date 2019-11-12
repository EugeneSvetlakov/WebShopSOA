using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Interfaces.Services
{
    /// <summary>
    /// Интерфейс для работы с сотрудниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeView> GetAll();

        /// <summary>
        /// Получение сотрудника по Id
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        EmployeeView GetById(int id);

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="model"></param>
        void AddNew(EmployeeView model);

        EmployeeView Update(int id, EmployeeView employee);

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        void Commit();
    }
}
