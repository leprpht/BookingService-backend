namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewCreationDto
{
    public Guid UserId { get; set; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required Guid PropertyId { get; init; }
}