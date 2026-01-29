using System.Text.Json.Serialization;

namespace BookingService.Housing.DTOs;

public sealed class HousingCreationDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("address")]
    public required string Address { get; init; }
    
    [JsonPropertyName("city")]
    public required string City { get; init; }
    
    [JsonPropertyName("state")]
    public required string State { get; init; }
    
    [JsonPropertyName("country")]
    public required string Country { get; init; }
    
    [JsonPropertyName("capacity")]
    public required int Capacity { get; init; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; init; }
}