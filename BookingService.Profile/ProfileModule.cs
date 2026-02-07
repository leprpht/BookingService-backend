using BookingService.Profile.Data;
using BookingService.Profile.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Profile;

public static class ProfileModule
{
    public static IServiceCollection RegisterProfileModule(this IServiceCollection services)
    {
        services
            .AddScoped<IGuestService, GuestService>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}