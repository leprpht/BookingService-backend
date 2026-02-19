namespace BookingService.Housing.DTOs.Stay;

public sealed class StayDto
{
    public Guid Id { get; init; }
    public required string PropertyName { get; set; }
    public required string UnitName { get; set; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public double Price { get; init; }
    public required string Status { get; set; }
}