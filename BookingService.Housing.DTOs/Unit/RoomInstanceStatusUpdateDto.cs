namespace BookingService.Housing.DTOs.Unit;

public sealed class RoomInstanceStatusUpdateDto
{
    public required Guid Id { get; init; }
    public required string Status { get; init; }
}