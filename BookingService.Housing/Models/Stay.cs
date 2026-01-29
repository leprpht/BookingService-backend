namespace BookingService.Housing.Models;

public sealed record Stay(
    int Id,
    int HousingId,
    HousingInfo Housing,
    DateOnly StartDate,
    DateOnly EndDate);