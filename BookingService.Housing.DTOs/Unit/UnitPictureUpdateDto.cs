namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitPictureUpdateDto
{
    public required int Id { get; init; }
    public required int UnitId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}