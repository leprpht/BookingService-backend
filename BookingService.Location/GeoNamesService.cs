using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace BookingService.Location;

public sealed class GeoNamesService(
    HttpClient httpClient,
    IOptions<GeoNamesOptions> options
) : IGeoNamesService
{
    private readonly GeoNamesOptions _options = options.Value;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Feature codes that represent meaningful populated places.
    // PPLC = capital, PPLA* = admin-division capitals, PPL = populated place.
    private static readonly string[] PopulatedPlaceCodes =
        ["PPLC", "PPLA", "PPLA2", "PPLA3", "PPL"];

    public async Task<IEnumerable<GeoNamesPlace>?> FindCitiesAsync(string city, string? countryHint = null,
        int maxRows = 5)
    {
        if (string.IsNullOrWhiteSpace(city))
            return null;

        try
        {
            var url = BuildSearchUrl(city, countryHint, maxRows * 4);
            var response = await httpClient.GetFromJsonAsync<GeoNamesSearchResponse>(url, JsonOptions);

            var best = response?.Geonames
                .OrderBy(p => Array.IndexOf(PopulatedPlaceCodes, p.FeatureCode) is var idx && idx >= 0 ? idx : 99)
                .ThenByDescending(p => p.Population)
                .Take(maxRows);

            return best;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling GeoNames API: {ex.Message}");
            return null;
        }
    }

    private string BuildSearchUrl(string city, string? countryHint, int maxRows)
    {
        var url =
            $"{_options.BaseUrl}/searchJSON" +
            $"?name_startsWith={Uri.EscapeDataString(city)}" +
            "&featureClass=P" +
            $"&maxRows={maxRows}" +
            "&style=MEDIUM" +
            $"&username={Uri.EscapeDataString(_options.Username)}";

        if (!string.IsNullOrWhiteSpace(countryHint))
            url += $"&countryBias={Uri.EscapeDataString(countryHint)}";

        return url;
    }
}