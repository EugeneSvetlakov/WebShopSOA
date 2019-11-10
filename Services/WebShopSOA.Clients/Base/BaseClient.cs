using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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
        
    }
}
