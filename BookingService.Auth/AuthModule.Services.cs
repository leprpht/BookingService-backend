using BookingService.Auth.Data;
using BookingService.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Auth;

public static partial class AuthModule
{
    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddScoped<IRefreshTokenService, RefreshTokenService>()
            .AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}