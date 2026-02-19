namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyPictureUpdateDto
{
    public required Guid Id { get; init; }
    public required Guid UnitId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}