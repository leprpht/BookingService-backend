namespace BookingService.Housing.DTOs.Unit;

public sealed class RoomInstanceDto
{
    public Guid Id { get; init; }
    public required string RoomNumber { get; init; }
    public required string Status { get; init; }
    public Guid UnitId { get; init; }
}