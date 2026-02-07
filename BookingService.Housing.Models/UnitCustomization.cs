namespace BookingService.Housing.Models;

public sealed class UnitCustomization
{
    public int Id { get; set; }
    public required CustomizationType Type { get; init; }
    public required string Text { get; init; }
    
    public int UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
}