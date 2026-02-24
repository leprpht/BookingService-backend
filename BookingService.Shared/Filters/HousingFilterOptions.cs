using BookingService.Shared.Requests;

namespace BookingService.Shared.Filters;

public sealed class HousingFilterOptions
{
    public required PeriodRequest Period { get; init; }
    public required string SearchQuery { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public double? MaxPrice { get; set; }
    public double? MinPrice { get; set; }
    public List<Guid>? Tags { get; set; }
    public double? MinRating { get; set; }
    public List<int>? Capacities { get; set; }
}