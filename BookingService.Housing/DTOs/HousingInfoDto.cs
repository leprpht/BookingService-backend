using System.Text.Json.Serialization;

namespace BookingService.Housing.DTOs;

public sealed class HousingInfoDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
    
    [JsonPropertyName("address")]
    public string Address { get; init; } = string.Empty;
    
    [JsonPropertyName("city")]
    public string City { get; init; } = string.Empty;
    
    [JsonPropertyName("state")]
    public string State { get; init; } = string.Empty;
    
    [JsonPropertyName("country")]
    public string Country { get; init; } = string.Empty;
    
    [JsonPropertyName("available")]
    public int? Available { get; init; }
}