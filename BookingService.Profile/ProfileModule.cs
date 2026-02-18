using BookingService.Profile.Data;
using BookingService.Profile.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Profile;

public static class ProfileModule
{
    public static IServiceCollection RegisterProfileModule(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserStayRepository, UserStayRepository>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserStayService, UserStayService>();

        return services;
    }
}