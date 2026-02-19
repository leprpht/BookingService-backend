namespace BookingService.Housing.DTOs.Unit;

public class UnitAdditionalServicesDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public Guid UnitId { get; set; }
}