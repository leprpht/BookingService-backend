using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Hubs;

public static class SignalRModule
{
    public static IServiceCollection RegisterSignalRModule(this IServiceCollection services)
    {
        services
            .AddSignalR();
        
        return services;
    }
}