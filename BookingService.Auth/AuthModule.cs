using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Auth;

public static partial class AuthModule
{
    public static IServiceCollection RegisterAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterServices()
            .AddAuthenticationConfig(configuration)
            .AddAuthorizationConfig();
        
        return services;
    }
}