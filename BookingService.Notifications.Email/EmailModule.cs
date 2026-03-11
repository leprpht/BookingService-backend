using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BookingService.Notifications.Email;

public static class EmailModule
{
    public static IServiceCollection RegisterEmailModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailServiceOptions>(
            configuration.GetSection(EmailServiceOptions.SectionName));
        
        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}