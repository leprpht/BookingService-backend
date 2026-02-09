namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewCreationDto
{
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required int UserId { get; init; }
    public required int PropertyId { get; init; }
}