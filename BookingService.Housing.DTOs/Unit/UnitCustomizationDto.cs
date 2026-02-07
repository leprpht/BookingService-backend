namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitCustomizationDto
{
    public required string Type { get; init; }
    public required List<UnitCustomizationGroupedDto> Customizations { get; init; }
}