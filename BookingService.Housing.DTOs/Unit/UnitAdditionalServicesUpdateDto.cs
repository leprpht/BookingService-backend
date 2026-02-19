namespace BookingService.Housing.DTOs.Unit;

public class UnitAdditionalServicesUpdateDto
{
    public required Guid Id { get; init; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public required Guid UnitId { get; init; }
}