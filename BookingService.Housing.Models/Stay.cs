namespace BookingService.Housing.Models;

public sealed class Stay
{
    public int Id { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public decimal TotalPrice { get; init; }
    public StayStatus Status { get; init; }
    
    public int UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
    
    public int UserId { get; init; }
}