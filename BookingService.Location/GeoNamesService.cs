using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace BookingService.Location;

public sealed class GeoNamesService(
    HttpClient httpClient,
    IOptions<GeoNamesOptions> options
    ) : IGeoNamesService
{
    private readonly GeoNamesOptions _options = options.Value;

    // Feature codes that represent meaningful populated places.
    // PPLC = capital, PPLA* = admin-division capitals, PPL = populated place.
    private static readonly string[] PopulatedPlaceCodes =
        ["PPLC", "PPLA", "PPLA2", "PPLA3", "PPL"];

    public async Task<GeoNamesPlace?> FindCityAsync(string city, string? countryHint = null)
    {
        if (string.IsNullOrWhiteSpace(city))
            return null;

        try
        {
            var url = BuildSearchUrl(city, countryHint);
            var response = await httpClient.GetFromJsonAsync<GeoNamesSearchResponse>(url);
            
            var best = response?.Geonames
                .OrderBy(p => Array.IndexOf(PopulatedPlaceCodes, p.FeatureCode) is var idx && idx >= 0 ? idx : 99)
                .ThenByDescending(p => p.Population)
                .FirstOrDefault();
            
            return best;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling GeoNames API: {ex.Message}");
            return null;
        }
    }
    
    private string BuildSearchUrl(string city, string? countryHint)
    {
        var url =
            $"{_options.BaseUrl}/searchJSON" +
            $"?q={Uri.EscapeDataString(city)}" +
            "&featureClass=P" +
            "&maxRows=5" +
            "&style=SHORT" +
            $"&username={Uri.EscapeDataString(_options.Username)}";
        
        if (!string.IsNullOrWhiteSpace(countryHint))
            url += $"&countryBias={Uri.EscapeDataString(countryHint)}";

        return url;
    }
}