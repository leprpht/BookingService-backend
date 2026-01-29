namespace BookingService.Housing.Utils;

public sealed class FilterOptions
{
    public string? Name { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public bool? AvailableOnly { get; init; }
}