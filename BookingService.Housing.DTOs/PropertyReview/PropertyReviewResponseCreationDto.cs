namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewResponseCreationDto
{
    public Guid UserId { get; set; }
    public required int Rating { get; init; }
    public required string Comment { get; init; }
    public Guid PropertyReviewId { get; init; }
    public required Guid PropertyId { get; init; }
}