namespace BookingService.Location;

public sealed class LocationNormalizationService(IGeoNamesService geoNames) : ILocationNormalizationService
{
    public async Task<LocationNormalizationResult> NormalizeAsync(string city, string? state, string country)
    {
        var place = await geoNames.FindCityAsync(city, country);

        if (place is null)
        {
            Console.WriteLine($"GeoNames could not find a match for {city}, {state}, {country}");
            return Fallback(city, state, country);
        }

        var normalizedState = string.IsNullOrWhiteSpace(place.AdminName1)
            ? state
            : place.AdminName1;

        Console.WriteLine($"GeoNames normalized {city}, {state}, {country} to {place.Name}, {normalizedState}, {place.CountryName}");

        return new LocationNormalizationResult
        {
            City = place.Name,
            State = normalizedState,
            Country = place.CountryName,
            CountryCode = place.CountryCode,
            IsNormalized = true
        };
    }
    
    private static LocationNormalizationResult Fallback(string city, string? state, string country)
        => new()
        {
            City = city,
            State = state,
            Country = country,
            CountryCode = null,
            IsNormalized = false
        };
}