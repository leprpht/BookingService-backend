namespace BookingService.Housing.DTOs.Unit;

public sealed class RoomInstanceUpdateDto
{
    public required Guid Id { get; init; }
    public required string RoomNumber { get; init; }
}