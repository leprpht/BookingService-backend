namespace BookingService.Housing.Models;

public sealed class HousingInfo()
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public decimal Price { get; init; }
    public List<Stay> Stays { get; init; } = new();
}