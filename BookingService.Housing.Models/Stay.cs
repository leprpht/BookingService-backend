namespace BookingService.Housing.Models;

public sealed class Stay
{
    public Guid Id { get; set; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public double TotalPrice { get; init; }
    public StayStatus Status { get; set; }
    public double Price { get; set; }
    
    public Guid UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
    
    public Guid UserId { get; set; }
}