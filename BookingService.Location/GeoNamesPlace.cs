namespace BookingService.Location;

public sealed class GeoNamesPlace
{
    public string Name { get; init; } = string.Empty;
    
    public string AdminName1 { get; init; } = string.Empty;
    
    public string CountryName { get; init; } = string.Empty;
    
    public string CountryCode { get; init; } = string.Empty;
    
    public string Lat { get; init; } = string.Empty;
    
    public string Lng { get; init; } = string.Empty;

    public string FeatureClass { get; init; } = string.Empty;
    
    public string FeatureCode { get; init; } = string.Empty;

    public long Population { get; init; }
}