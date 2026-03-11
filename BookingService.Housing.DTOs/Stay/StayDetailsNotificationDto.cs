namespace BookingService.Housing.DTOs.Stay;

public sealed class StayDetailsNotificationDto
{
    public required Guid Id { get; init; }
    public required string City { get; init; }
    public string? State { get; init; }
    public required string Country { get; init; }
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public required Guid PropertyId { get; init; }
    public required string PropertyName { get; init; }
}