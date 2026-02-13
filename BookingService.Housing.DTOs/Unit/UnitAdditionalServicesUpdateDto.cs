namespace BookingService.Housing.DTOs.Unit;

public class UnitAdditionalServicesUpdateDto
{
    public required int Id { get; init; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public required int UnitId { get; init; }
}