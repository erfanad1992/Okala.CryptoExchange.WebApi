using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Okala.CryptoExchange.EfPersistance.DI;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceEntityFrameworkServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"),
              x => x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo"));
        });
        services.AddScoped<DbContext>((sp) => sp.GetRequiredService<ApplicationDbContext>());
    }
}
