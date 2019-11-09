using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopSOA.ViewModels;

namespace WebShopSOA.Infrastructure.Interfaces
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
        /// Сохранение изменений
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="model"></param>
        void AddNew(EmployeeView model);

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
