using Azure;
using Azure.Communication.Email;
using BookingService.Housing.DTOs.Stay;
using Microsoft.Extensions.Options;

namespace BookingService.Notifications.Email;

public class EmailService(IOptions<EmailServiceOptions> options) : IEmailService
{
    private readonly EmailServiceOptions _options = options.Value;

    public Task SendTripReminderEmailAsync(TripReminderEmailDto dto)
    {
        var state = string.IsNullOrWhiteSpace(dto.State) ? "" : $", {dto.State}";
        
        var html = EmailTemplateLoader
            .LoadTemplate("TripReminderEmailTemplate.html")
            .Replace("{{FirstName}}", dto.FirstName)
            .Replace("{{BookingId}}", dto.BookingId.ToString())
            .Replace("{{PropertyName}}", dto.PropertyName)
            .Replace("{{City}}", dto.City)
            .Replace("{{State}}", state)
            .Replace("{{Country}}", dto.Country)
            .Replace("{{UnitName}}", dto.UnitName)
            .Replace("{{RoomNumber}}", dto.RoomNumber)
            .Replace("{{CheckIn}}", dto.CheckIn.ToString("dd MMM yyyy"))
            .Replace("{{CheckOut}}", dto.CheckOut.ToString("dd MMM yyyy"));

        return SendAsync(dto.Email, "Reminder: Your trip is tomorrow!", html);
    }

    public Task SendBookingConfirmationToGuestAsync(BookingConfirmationEmailDto dto)
    {
        var location = string.IsNullOrWhiteSpace(dto.State)
            ? $"{dto.City}, {dto.Country}"
            : $"{dto.City}, {dto.State}, {dto.Country}";

        var html = EmailTemplateLoader
            .LoadTemplate("BookingConfirmationEmailTemplate.html")
            .Replace("{{FirstName}}", dto.FirstName)
            .Replace("{{LastName}}", dto.LastName)
            .Replace("{{BookingId}}", dto.BookingId.ToString())
            .Replace("{{PropertyName}}", dto.PropertyName)
            .Replace("{{PropertyAddress}}", dto.PropertyAddress)
            .Replace("{{City}}", dto.City)
            .Replace("{{Country}}", location)
            .Replace("{{UnitName}}", dto.UnitName)
            .Replace("{{RoomNumber}}", dto.RoomNumber)
            .Replace("{{CheckIn}}", dto.CheckIn.ToString("dd MMM yyyy"))
            .Replace("{{CheckOut}}", dto.CheckOut.ToString("dd MMM yyyy"))
            .Replace("{{TotalPrice}}", $"${dto.TotalPrice:F2}");

        return SendAsync(dto.Email, $"Booking Received – {dto.PropertyName}", html);
    }

    public Task SendNewBookingNotificationToHostAsync(HostBookingNotificationEmailDto dto)
    {
        var html = EmailTemplateLoader
            .LoadTemplate("HostNewBookingEmailTemplate.html")
            .Replace("{{HostFirstName}}", dto.HostFirstName)
            .Replace("{{GuestFullName}}", dto.GuestFullName)
            .Replace("{{BookingId}}", dto.BookingId.ToString())
            .Replace("{{PropertyName}}", dto.PropertyName)
            .Replace("{{UnitName}}", dto.UnitName)
            .Replace("{{RoomNumber}}", dto.RoomNumber)
            .Replace("{{CheckIn}}", dto.CheckIn.ToString("dd MMM yyyy"))
            .Replace("{{CheckOut}}", dto.CheckOut.ToString("dd MMM yyyy"))
            .Replace("{{TotalPrice}}", $"${dto.TotalPrice:F2}");

        return SendAsync(dto.HostEmail, $"New Reservation – {dto.PropertyName}", html);
    }

    public Task SendBookingStatusChangedToGuestAsync(BookingStatusChangedEmailDto dto)
    {
        var (color, subtitleColor, icon, statusMessage, bodyText) = ResolveStatusContent(dto.NewStatus);

        var html = EmailTemplateLoader
            .LoadTemplate("BookingStatusChangedEmailTemplate.html")
            .Replace("{{StatusColor}}", color)
            .Replace("{{StatusSubtitleColor}}", subtitleColor)
            .Replace("{{StatusIcon}}", icon)
            .Replace("{{NewStatus}}", dto.NewStatus)
            .Replace("{{StatusMessage}}", statusMessage)
            .Replace("{{BodyText}}", bodyText)
            .Replace("{{FirstName}}", dto.FirstName)
            .Replace("{{BookingId}}", dto.BookingId.ToString())
            .Replace("{{PropertyName}}", dto.PropertyName)
            .Replace("{{City}}", dto.City)
            .Replace("{{Country}}", dto.Country)
            .Replace("{{CheckIn}}", dto.CheckIn.ToString("dd MMM yyyy"))
            .Replace("{{CheckOut}}", dto.CheckOut.ToString("dd MMM yyyy"));

        var subject = dto.NewStatus switch
        {
            "Confirmed" => $"Great news! Your booking at {dto.PropertyName} is confirmed",
            "Cancelled" => $"Your booking at {dto.PropertyName} has been cancelled",
            "Completed" => $"We hope you enjoyed your stay at {dto.PropertyName}",
            _ => $"Booking status update – {dto.PropertyName}"
        };

        return SendAsync(dto.Email, subject, html);
    }

    private Task<EmailSendOperation> SendAsync(string recipientEmail, string subject, string htmlBody)
    {
        var client = new EmailClient(_options.ConnectionString);

        var message = new EmailMessage(
            senderAddress: _options.SenderAddress,
            content: new EmailContent(subject)
            {
                Html = htmlBody
            },
            recipientAddress: recipientEmail);

        return client.SendAsync(WaitUntil.Completed, message);
    }

    private static (string color, string subtitleColor, string icon, string statusMessage, string bodyText) ResolveStatusContent(string status) =>
        status switch
    {
        "Confirmed" => (
            "#0f5c2e",
            "#a8d5b5",
            "✓",
            "Your host has confirmed your reservation.",
            "Great news! Your booking has been confirmed by the host. Everything is set — we look forward to your stay."),

        "Cancelled" => (
            "#c0392b",
            "#f5b7b1",
            "✕",
            "Your reservation has been cancelled.",
            "We're sorry to let you know that this booking has been cancelled. If you did not request this cancellation, please contact support."),

        "Completed" => (
            "#7d3c98",
            "#d2b4de",
            "★",
            "Your stay has been marked as completed.",
            "We hope you had a wonderful experience! Thank you for choosing BookingService. We'd love to hear your feedback — don't forget to leave a review."),

        _ => (
            "#555555",
            "#cccccc",
            "ℹ",
            "Your booking status has been updated.",
            "There has been an update to your reservation. Please log in for full details.")
    };
}