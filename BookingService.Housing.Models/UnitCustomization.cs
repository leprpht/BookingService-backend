namespace BookingService.Housing.Models;

public sealed class UnitCustomization
{
    public Guid Id { get; set; }
    public required CustomizationType Type { get; init; }
    public required string Text { get; init; }
    
    public Guid UnitId { get; set; }
    public Unit Unit { get; init; } = null!;
}