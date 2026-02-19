namespace BookingService.Housing.DTOs.Stay;

public sealed class StayUpdateDto
{
    public Guid Id { get; init; }
    public required Guid UnitId { get; init; }
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public required string Status { get; init; }
}