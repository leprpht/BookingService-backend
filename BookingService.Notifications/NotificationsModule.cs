using BookingService.Notifications.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Notifications;

public static class NotificationsModule
{
    public static IServiceCollection RegisterNotificationsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterEmailModule(configuration);

        services
            .AddScoped<INotificationService, NotificationService>();

        return services;
    }
}