namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyInfoDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? PictureUrl { get; init; }
}