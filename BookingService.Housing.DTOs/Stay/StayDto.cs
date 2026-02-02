namespace BookingService.Housing.DTOs.Stay;

public sealed class StayDto
{
    public int Id { get; init; }
    public required string PropertyName { get; init; }
    public required string UnitName { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public required string Status { get; init; }
}