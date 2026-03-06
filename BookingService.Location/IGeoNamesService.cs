namespace BookingService.Location;

public interface IGeoNamesService
{
    Task<GeoNamesPlace?> FindCityAsync(string city, string? countryHint = null);
}