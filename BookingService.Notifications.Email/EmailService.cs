using Azure;
using Azure.Communication.Email;
using BookingService.Housing.DTOs.Stay;
using Microsoft.Extensions.Options;

namespace BookingService.Notifications.Email;

public class EmailService(IOptions<EmailServiceOptions> options) : IEmailService
{
    private readonly EmailServiceOptions _options = options.Value;
    
    public Task SendTripReminderEmailAsync(StayNotificationDto stay)
    {
        var emailClient = new EmailClient(_options.ConnectionString);
        
        var template = EmailTemplateLoader
            .LoadTemplate("TripReminderEmailTemplate.html")
            .Replace("{{FirstName}}", stay.FirstName)
            .Replace("{{PropertyName}}", stay.Stays[0].PropertyName)
            .Replace("{{City}}", stay.Stays[0].City)
            .Replace("{{Country}}", stay.Stays[0].Country)
            .Replace("{{From}}", stay.Stays[0].From.ToString("dd.MM.yyyy"))
            .Replace("{{To}}", stay.Stays[0].To.ToString("dd.MM.yyyy"));

        var emailMessage = new EmailMessage(
            senderAddress: _options.SenderAddress,
            content: new EmailContent("Test Email") 
            {
                PlainText = "Hello world via email.",
                Html = template
            },
            recipientAddress: stay.Email
            );

        return emailClient.SendAsync(WaitUntil.Completed, emailMessage);
    }
}