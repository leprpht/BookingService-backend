using Azure;
using Azure.Communication.Email;
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
            .Replace("{{name}}", stay.FirstName)
            .Replace("{{destination}}", stay.Stays[0].PropertyName)
            .Replace("{{date}}", stay.Stays[0].From.ToString("dd.MM.yyyy"));

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