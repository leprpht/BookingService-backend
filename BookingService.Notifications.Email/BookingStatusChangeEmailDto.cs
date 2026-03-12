namespace BookingService.Notifications.Email;

public sealed class BookingStatusChangedEmailDto
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required Guid BookingId { get; init; }
    public required string PropertyName { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public required DateOnly CheckIn { get; init; }
    public required DateOnly CheckOut { get; init; }
    public required string NewStatus { get; init; }
}