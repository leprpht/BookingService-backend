namespace BookingService.Housing.Models;

public sealed class Stay
{
    public Guid Id { get; set; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public double TotalPrice { get; set; }
    public StayStatus Status { get; set; }

    public Guid RoomInstanceId { get; set; }
    public RoomInstance RoomInstance { get; init; } = null!;

    public Guid UserId { get; set; }
}