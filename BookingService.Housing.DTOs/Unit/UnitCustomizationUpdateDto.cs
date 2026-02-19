using BookingService.Housing.Models;

namespace BookingService.Housing.DTOs.Unit;

public class UnitCustomizationUpdateDto
{
    public required Guid Id { get; init; }
    public required CustomizationType Type { get; init; }
    public required string Text { get; init; }
}