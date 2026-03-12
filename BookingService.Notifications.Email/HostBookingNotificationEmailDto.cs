namespace BookingService.Notifications.Email;

public sealed class HostBookingNotificationEmailDto
{
    public required string HostEmail { get; init; }
    public required string HostFirstName { get; init; }
    public required string GuestFullName { get; init; }
    public required Guid BookingId { get; init; }
    public required string PropertyName { get; init; }
    public required string UnitName { get; init; }
    public required string RoomNumber { get; init; }
    public required DateOnly CheckIn { get; init; }
    public required DateOnly CheckOut { get; init; }
    public required double TotalPrice { get; init; }
}