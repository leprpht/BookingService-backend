using BookingService.Housing.DTOs.Stay;

namespace BookingService.Notifications.Email;

public interface IEmailService
{
    public Task SendTripReminderEmailAsync(StayNotificationDto stay);
}