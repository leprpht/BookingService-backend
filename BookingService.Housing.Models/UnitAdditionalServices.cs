namespace BookingService.Housing.Models;

public class UnitAdditionalServices
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
}