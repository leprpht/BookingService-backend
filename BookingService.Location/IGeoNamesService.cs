namespace BookingService.Location;

public interface IGeoNamesService
{
    Task<IEnumerable<GeoNamesPlace>?> FindCitiesAsync(string city, string? countryHint = null, int maxRows = 5);
}