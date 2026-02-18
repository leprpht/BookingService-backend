namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewResponseCreationDto
{
    public int UserId { get; set; }
    public required int Rating { get; init; }
    public required string Comment { get; init; }
    public int PropertyReviewId { get; init; }
    public required int PropertyId { get; init; }
}