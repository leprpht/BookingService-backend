namespace BookingService.Housing.Models;

public class UnitAdditionalServices
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Guid UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
}