using BookingService.Housing.Models;

namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitCustomizationCreationDto
{
    public required CustomizationType Type { get; init; }
    public required string Text { get; init; }
}