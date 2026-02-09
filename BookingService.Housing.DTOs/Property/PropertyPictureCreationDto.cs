namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyPictureCreationDto
{
    public required int PropertyId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}