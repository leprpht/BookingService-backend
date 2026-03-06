namespace BookingService.Location;

public interface ILocationNormalizationService
{
    Task<LocationNormalizationResult> NormalizeAsync(string city, string? state, string country);
}