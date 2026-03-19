using System.Text.Json.Serialization;

namespace BookingService.Location;

public sealed class GeoNamesSearchResponse
{
    [JsonPropertyName("totalResultsCount")]
    public int TotalResultsCount { get; init; }

    [JsonPropertyName("geonames")] public List<GeoNamesPlace> Geonames { get; init; } = [];
}