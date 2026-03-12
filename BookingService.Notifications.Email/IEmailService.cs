using BookingService.Housing.DTOs.Stay;

namespace BookingService.Notifications.Email;

public interface IEmailService
{
    Task SendTripReminderEmailAsync(TripReminderEmailDto dto);
    Task SendBookingConfirmationToGuestAsync(BookingConfirmationEmailDto dto);
    Task SendNewBookingNotificationToHostAsync(HostBookingNotificationEmailDto dto);
    Task SendBookingStatusChangedToGuestAsync(BookingStatusChangedEmailDto dto);
}