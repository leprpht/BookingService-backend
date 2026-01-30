namespace BookingService.Shared;

public sealed class FilterOptions
{
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public string? Name { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
    public decimal? MaxPrice { get; init; }
    public decimal? MinPrice { get; init; }
}