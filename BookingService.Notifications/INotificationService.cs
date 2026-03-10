namespace BookingService.Notifications;

public interface INotificationService
{
    Task SendTripRemindersAsync();
}