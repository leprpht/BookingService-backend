using BookingService.Housing.Models;

namespace BookingService.Notifications;

public sealed class UserNotificationDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public List<StayNotificationDto> Stays { get; init; } = [];
}