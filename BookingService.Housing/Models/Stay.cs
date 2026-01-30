namespace BookingService.Housing.Models;

public sealed class Stay
{
    public int Id { get; init; }
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public int HousingInfoId { get; init; }
}