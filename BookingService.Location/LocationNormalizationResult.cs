namespace BookingService.Location;

public sealed class LocationNormalizationResult
{
    public required string City { get; init; }
    
    public string? State { get; init; }
    
    public required string Country { get; init; }
    
    public string? CountryCode { get; init; }
    
    public bool IsNormalized { get; init; }
}