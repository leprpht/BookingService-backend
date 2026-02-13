namespace BookingService.Housing.DTOs.Unit;

public class UnitAdditionalServicesDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public int UnitId { get; set; }
}