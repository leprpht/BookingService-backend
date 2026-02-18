namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyCreationDto
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string Country { get; init; }
    public string Description { get; init; } = string.Empty;
}