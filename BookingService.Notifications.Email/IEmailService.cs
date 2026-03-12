using BookingService.Housing.DTOs.Stay;

namespace BookingService.Notifications.Email;

public interface IEmailService
{
    Task SendTripReminderEmailAsync(StayNotificationDto stay);
    Task SendBookingConfirmationToGuestAsync(BookingConfirmationEmailDto dto);
    Task SendNewBookingNotificationToHostAsync(HostBookingNotificationEmailDto dto);
    Task SendBookingStatusChangedToGuestAsync(BookingStatusChangedEmailDto dto);
}