using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using WebShopSOA.DAL;

namespace WebShopSOA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
            .WriteTo.RollingFile($".\\Logs\\WebshopSOA[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
            .WriteTo.File(new JsonFormatter(",", true), $".\\Logs\\json\\WebshopSOA[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
            .WriteTo.Seq("http://localhost:5341")
            )
            .Build();
    }
}
