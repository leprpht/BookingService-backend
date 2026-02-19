namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitUpdateDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    public Guid PropertyId { get; set; }
}