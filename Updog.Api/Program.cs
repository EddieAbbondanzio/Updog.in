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

            var adminConfig = host.Services.GetService<IAdminConfig>();

            using (var scope = host.Services.CreateScope()) {
                // Perform any database migrations
                scope.ServiceProvider.GetRequiredService<DatabaseMigrationRunner>().MigrateUp();

                // Create the admin account.
                var admin = scope.ServiceProvider.GetService<CommandHandler<AdminRegisterOrUpdateCommand>>().Execute(new AdminRegisterOrUpdateCommand(adminConfig));
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
