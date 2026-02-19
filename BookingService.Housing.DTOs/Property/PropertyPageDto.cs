using System.Text.Json.Serialization;

namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyPageDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    
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
    
    [JsonPropertyName("price")]
    public required decimal Price { get; init; }
    
    [JsonPropertyName("picture")]
    public required string? PictureUrl { get; init; }
    
    [JsonPropertyName("rating")]
    public required double Rating { get; init; }
    
    [JsonPropertyName("reviewsCount")]
    public required int ReviewCount { get; init; }
}