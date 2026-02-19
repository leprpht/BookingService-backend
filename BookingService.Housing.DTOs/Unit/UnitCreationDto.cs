namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitCreationDto
{
    public required string Name { get; init; }
    public int Capacity { get; init; }
    public decimal Price { get; init; }
    public int Size { get; init; }
    public Guid PropertyId { get; init; }
}