namespace BookingService.Housing.DTOs.Stay;

public sealed class StayUpdateDto
{
    public int Id { get; init; }
    public required int UnitId { get; init; }
    public required int GuestId { get; init; }
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public decimal TotalPrice { get; init; }
    public required string Status { get; init; }
}