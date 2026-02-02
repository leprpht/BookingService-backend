namespace BookingService.Shared;

public sealed class HousingFilterOptions
{
    public required PeriodRequest Period { get; init; }
    public string? Name { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public decimal? MaxPrice { get; init; }
    public decimal? MinPrice { get; init; }
}