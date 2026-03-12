namespace BookingService.Notifications.Email;

public sealed class BookingConfirmationEmailDto
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required Guid BookingId { get; init; }
    public required string PropertyName { get; init; }
    public required string PropertyAddress { get; init; }
    public required string City { get; init; }
    public string? State { get; init; }
    public required string Country { get; init; }
    public required string UnitName { get; init; }
    public required string RoomNumber { get; init; }
    public required DateOnly CheckIn { get; init; }
    public required DateOnly CheckOut { get; init; }
    public required double TotalPrice { get; init; }
}