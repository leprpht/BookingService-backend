using BookingService.Shared.Requests;

namespace BookingService.Shared.Filters;

public sealed class HousingFilterOptions
{
    public required PeriodRequest Period { get; init; }
    public string? Name { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public double? MaxPrice { get; init; }
    public double? MinPrice { get; init; }
    public List<int>? Tags { get; set; }
    public double? MinRating { get; init; }
}