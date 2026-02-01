namespace BookingService.Housing.DTOs;

public sealed class UnitCustomizationDto
{
    public required string Type { get; init; }
    public required List<string> Text { get; init; }
}