using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebShopSOA.Clients.Employees;
using WebShopSOA.Clients.Identity;
using WebShopSOA.Clients.Orders;
using WebShopSOA.Clients.Products;
using WebShopSOA.Clients.Values;
using WebShopSOA.DAL;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Infrastructure;
using WebShopSOA.Interfaces.Api;
using WebShopSOA.Interfaces.Services;
using WebShopSOA.Services.ShopProduct;
using WebShopSOA.Log4Net;
using WebShopSOA.Infrastructure.Middleware;

namespace WebShopSOA
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => 
            {
                options.Filters.Add(new SimpleActionFilterAttribute());
            });

            services.AddSingleton<IEmployeeService, EmployeesClient>();
            services.AddScoped<IProductService, ProductsClient>(); // Данные из БД
            services.AddScoped<IOrderService, OrdersClient>(); // Данные из БД

            services.AddScoped<IValuesService, ValuesClient>(); // Get Data throught Api Testing release

            #region Custom Identity Implementation

            //// доп настройка сервиса Аутентификации
            services.Configure<IdentityOptions>(o =>
            {
                o.Password.RequiredLength = 3;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.User.RequireUniqueEmail = true;
            });

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders();
            
            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            #endregion

            services.ConfigureApplicationCookie(o => {
                o.Cookie.Expiration = TimeSpan.FromDays(100);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // для доступа к Cookies

            services.AddScoped<ICartStore, CookieCartStore>(); // Сервис хранилища Корзины на Cookies
            services.AddScoped<ICartService, CartService>(); // Сервис Корзины на Cookies
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication(); // !!! размещать после UseStaticFiles()

            app.UseMiddleware<ErrorHandlingMiddleware>(); // Добавлние промежуточного ПО
            //app.UseMiddleware(typeof(ErrorHandlingMiddleware)); //Второй вариант добавления промежуточного ПО

            //app.UseMvcWithDefaultRoute();
            // Конфигурация инфраструктуры MVC
            app.UseMvc(routes =>
            {
                // Маршрут для Areas
                routes.MapRoute(
                     name: "areas",
                     template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                // Добавляем обработчик маршрута по умолчанию
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");
                // Маршрут по умолчанию состоит из трех частей разделенных '/'
                // Первой частью указывается имя контроллера,
                // второй - имя действия (метода) в контроллере,
                // третьей - опциональный параметр с именем 'id'
                // Если часть не указана - используется значение по умолчанию:
                // для контроллера имя 'Home'
                // для действия - 'Index'
            });

            var helloMessage = Configuration["CustomHellowWorld"];

            app.Run(async (context) =>
            {
                //await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync(helloMessage);
            });
        }
    }
}
