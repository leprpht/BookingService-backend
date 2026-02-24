namespace BookingService.Housing.DTOs.Stay;

public sealed class StayCreationDto
{
    public required Guid UnitId { get; init; }
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public required string Status { get; init; }
    public List<Guid>? AdditionalServiceIds { get; init; }
}