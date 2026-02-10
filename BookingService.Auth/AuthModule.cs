using BookingService.Auth.Data;
using BookingService.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Auth;

public static class AuthModule
{
    public static IServiceCollection RegisterAuthModule(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}