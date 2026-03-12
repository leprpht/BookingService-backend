namespace BookingService.Notifications;

public interface INotificationService
{
    Task SendTripRemindersAsync();
    Task SendNewBookingNotificationsAsync(Guid stayId);
    Task SendBookingStatusChangedNotificationAsync(Guid stayId, string newStatus);
}