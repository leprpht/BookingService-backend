using BookingService.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Auth;

public static class AuthModule
{
    public static IServiceCollection RegisterAuthModule(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}