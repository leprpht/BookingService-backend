namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitPictureUpdateDto
{
    public required Guid Id { get; init; }
    public required Guid UnitId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}