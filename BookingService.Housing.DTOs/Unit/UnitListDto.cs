namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitListDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public int Capacity { get; init; }
    public decimal Price { get; init; }
    public int Size { get; init; }
    public bool IsAvailable { get; init; }
}