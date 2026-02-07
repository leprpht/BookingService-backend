using BookingService.Housing.Models;

namespace BookingService.Housing.DTOs.Unit;

public class UnitCustomizationUpdateDto
{
    public required int Id { get; init; }
    public required CustomizationType Type { get; init; }
    public required string Text { get; init; }
}