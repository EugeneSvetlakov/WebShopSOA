using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebShopSOA.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration config, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(config["ClientAddress"])
            };

            var headers = _Client.DefaultRequestHeaders.Accept;

            headers.Clear();

            headers.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<T> GetAsync<T>(string url, CancellationToken Cancell = default) where T : new()
        {
            var response = await _Client.GetAsync(url, Cancell);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(Cancell);
            return new T();
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancell = default)
        {
            var response = await _Client.PostAsJsonAsync(url, item, Cancell);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancell = default)
        {
            var response = await _Client.PutAsJsonAsync(url, item, Cancell);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancell = default) => 
            await _Client.DeleteAsync(url, Cancell);
    }
}
