namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewCreationDto
{
    public int UserId { get; set; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required int PropertyId { get; init; }
}