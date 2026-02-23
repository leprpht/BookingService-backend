namespace BookingService.Housing.Models;

public sealed class RoomInstance
{
    public Guid Id { get; set; }
    public required string RoomNumber { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Available;

    public Guid UnitId { get; set; }
    public Unit Unit { get; init; } = null!;

    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
}