namespace BookingService.Housing.DTOs;

public sealed class StayDto
{
    public int Id { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
}