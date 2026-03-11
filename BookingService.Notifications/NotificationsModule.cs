using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Notifications;

public static class NotificationsModule
{
    public static IServiceCollection RegisterNotificationsModule(this IServiceCollection services)
    {
        services
            .AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}