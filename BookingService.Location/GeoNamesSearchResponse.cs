namespace BookingService.Location;

public sealed class GeoNamesSearchResponse
{
    public int TotalResultsCount { get; init; }
    
    public List<GeoNamesPlace> Geonames { get; init; } = [];
}