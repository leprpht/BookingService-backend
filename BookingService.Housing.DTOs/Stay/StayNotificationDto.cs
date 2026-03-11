namespace BookingService.Housing.DTOs.Stay;

public sealed class StayNotificationDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public List<StayDetailsNotificationDto> Stays { get; init; } = [];
}