namespace BookingService.Location;

public sealed class LocationNormalizationService(IGeoNamesService geoNames) : ILocationNormalizationService
{
    public async Task<LocationNormalizationResult> NormalizeAsync(string city, string? state, string country)
    {
        var places = await geoNames.FindCitiesAsync(city, country, 1);
        var place = places?.FirstOrDefault();

        if (place is null)
        {
            Console.WriteLine($"GeoNames could not find a match for {city}, {state}, {country}");
            return Fallback(city, state, country);
        }

        var normalizedState = string.IsNullOrWhiteSpace(place.AdminName1)
            ? state
            : place.AdminName1;

        Console.WriteLine(
            $"GeoNames normalized {city}, {state}, {country} to {place.Name}, {normalizedState}, {place.CountryName}");

        return new LocationNormalizationResult
        {
            City = place.Name,
            State = normalizedState,
            Country = place.CountryName,
            CountryCode = place.CountryCode,
            IsNormalized = true
        };
    }

    public async Task<List<string>> AutocompleteAsync(string city, int maxRows = 5)
    {
        var places = await geoNames.FindCitiesAsync(city, maxRows: maxRows);
        return places?.Select(p => $"{p.Name}, {p.CountryName}").Distinct().ToList() ?? [];
    }

    private static LocationNormalizationResult Fallback(string city, string? state, string country)
    {
        return new LocationNormalizationResult
        {
            City = city,
            State = state,
            Country = country,
            CountryCode = null,
            IsNormalized = false
        };
    }
}