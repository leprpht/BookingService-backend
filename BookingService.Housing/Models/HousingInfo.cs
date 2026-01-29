namespace BookingService.Housing.Models;

public sealed record HousingInfo(
    int Id,
    string Name,
    string Address,
    string City,
    string State,
    string Country,
    int Capacity);