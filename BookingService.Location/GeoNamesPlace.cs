using System.Text.Json.Serialization;

namespace BookingService.Location;

public sealed class GeoNamesPlace
{
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;

    [JsonPropertyName("adminName1")] public string AdminName1 { get; init; } = string.Empty;

    [JsonPropertyName("countryName")] public string CountryName { get; init; } = string.Empty;

    [JsonPropertyName("countryCode")] public string CountryCode { get; init; } = string.Empty;

    [JsonPropertyName("lat")] public string Lat { get; init; } = string.Empty;

    [JsonPropertyName("lng")] public string Lng { get; init; } = string.Empty;

    [JsonPropertyName("fclName")] public string FeatureClass { get; init; } = string.Empty;

    [JsonPropertyName("fcode")] public string FeatureCode { get; init; } = string.Empty;

    [JsonPropertyName("population")] public long Population { get; init; }
}