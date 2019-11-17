using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Services.ShopProduct;
using Swashbuckle.AspNetCore.Swagger;

namespace WebShopSOA.ServiceHosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
              

            services.AddDbContext<WebShopSOADbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            // подключенеи аутентификации
            services.AddIdentity<User, IdentityRole>(options =>
            {
                //Конфигурация возможна здесь
            })
                .AddEntityFrameworkStores<WebShopSOADbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddScoped<IProductService, SqlProductService>(); // Данные из БД
            services.AddScoped<IOrderService, SqlOrderService>(); // Данные из БД

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info { Title = "WebShopSOA.API ver.1", Version = "v1" });
                opt.IncludeXmlComments("WebShopSOA.ServiceHosting.xml"); // xml документация, генерируется самой MS Visual Studio на основе комментариев в коде
                opt.IncludeXmlComments(@"bin\Debug\netcoreapp2.2\WebShopSOA.Domain.xml"); // xml документация, генерируется самой MS Visual Studio на основе комментариев в коде
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("api-docs/v1/swagger.json", "WebShopSOA.API v1");
                opt.RoutePrefix = string.Empty;
            }); // Пользовательский интерфейс Swagger

            app.UseMvc();
        }
    }
}
