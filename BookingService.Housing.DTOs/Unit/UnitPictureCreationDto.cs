namespace BookingService.Housing.DTOs.Unit;

public sealed class UnitPictureCreationDto
{
    public required int UnitId { get; init; }
    public required string Url { get; init; }
    public bool IsCover { get; init; }
}