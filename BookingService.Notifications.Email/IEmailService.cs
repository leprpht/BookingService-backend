namespace BookingService.Notifications.Email;

public interface IEmailService
{
    public Task SendTripReminderEmailAsync(StayNotificationDto stay);
}