namespace BookingService.Housing.Models;

public sealed class Stay
{
    public int Id { get; set; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public double TotalPrice { get; init; }
    public StayStatus Status { get; set; }
    public double Price { get; set; }
    
    public int UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
    
    public int UserId { get; init; }
}