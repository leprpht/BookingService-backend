namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public int Capacity { get; init; }
    public decimal Price { get; init; }
    public int Size { get; init; }
    public List<UnitCustomizationDto> Customizations { get; init; } = new();
    public List<string> Pictures { get; init; } = new();
}