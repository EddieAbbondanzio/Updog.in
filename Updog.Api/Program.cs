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
using Updog.Application;
using Updog.Domain;
using Updog.Infrastructure;

namespace Updog.Api {
    public class Program {
        public static async Task Main(string[] args) {
            var host = CreateWebHostBuilder(args).Build();

            try {

                var adminConfig = host.Services.GetService<IAdminConfig>();
                var admin = host.Services.GetService<AdminRegistrar>().Handle(adminConfig);
            } catch {
                Console.WriteLine("Failed to create admin account");
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
