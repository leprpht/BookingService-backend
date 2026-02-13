namespace BookingService.Housing.DTOs.Unit;

public class UnitAdditionalServicesCreationDto
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int UnitId { get; init; }
}