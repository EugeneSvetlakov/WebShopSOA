using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebShopSOA.Interfaces.Api
{
    public interface IValuesService
    {
        /// <summary>
        /// Получить все элементы
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Get();

        /// <summary>
        /// Асинхронно получить все элементы
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAsync();

        string Get(int id);

        Task<string> GetAsync(int id);

        Uri Post(string value);

        Task<Uri> PostAsync(string value);

        HttpStatusCode Put(int id, string value);

        Task<HttpStatusCode> PutAsync(int id, string value);

        HttpStatusCode Delete(int id);

        Task<HttpStatusCode> DeleteAsync(int id);
    }
}
