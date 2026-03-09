namespace BookingService.Location;

public interface ILocationNormalizationService
{
    Task<LocationNormalizationResult> NormalizeAsync(string city, string? state, string country);
    Task<List<string>> AutocompleteAsync(string city, int maxRows = 5);
}