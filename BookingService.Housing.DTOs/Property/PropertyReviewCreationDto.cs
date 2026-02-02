namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyReviewCreationDto
{
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required int GuestId { get; init; }
    public required int PropertyId { get; init; }
}