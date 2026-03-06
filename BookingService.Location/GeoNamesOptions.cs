namespace BookingService.Location;

public sealed class GeoNamesOptions
{
    public const string SectionName = "GeoNames";

    public required string Username { get; init; }
    public string BaseUrl { get; init; } = "https://secure.geonames.org";
}