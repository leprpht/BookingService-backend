using BookingService.Auth.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Auth;

public static partial class AuthModule
{
    private static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("IdMatch", policy => policy.AddRequirements(new IdMatchRequirement()));

        return services;
    }
}