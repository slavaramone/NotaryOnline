using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace SharedLib.Extensions
{
	public static class OrmLiteConfigurationExtension
	{
        public static IServiceCollection ApplyFluentMigrations<TypeInAssembly>(this IServiceCollection services, string connectionString) where TypeInAssembly : Migration
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(TypeInAssembly).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            var sp = services.BuildServiceProvider();
            var runner = sp.GetService<IMigrationRunner>();
            runner?.MigrateUp();

            return services;
        }
    }
}
