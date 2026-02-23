using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Database;

public static class DatabaseModule
{
    public static IServiceCollection RegisterDatabaseModule(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BookingServiceDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sql =>
                {
                    sql.MigrationsAssembly("BookingService.Database");
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));

        return services;
    }
}