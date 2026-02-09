namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyPictureUpdateDto
{
    public required int Id { get; init; }
    public required int UnitId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}