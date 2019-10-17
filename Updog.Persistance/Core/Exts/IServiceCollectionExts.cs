using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Updog.Domain;

namespace Updog.Persistance {
    public static class IServiceCollectionExts {
        public static void AddDatabaseMigrations(this IServiceCollection services, string connectionString) {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                    rb.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(IServiceCollectionExts).Assembly).For.Migrations()
            );

            services.AddScoped<DatabaseMigrationRunner, FluentMigratorMigrationRunner>();
        }
    }
}