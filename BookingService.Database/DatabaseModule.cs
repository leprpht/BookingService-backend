using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Database;

public static class DatabaseModule
{
    public static IServiceCollection RegisterDatabaseModule(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<BookingServiceDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    m => m.MigrationsAssembly("BookingService.Database")));
        
        return services;
    }
}