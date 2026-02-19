namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewUpdateDto
{
    public Guid Id { get; set; }
    public int Rating { get; init; }
    public string? Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid PropertyId { get; init; }
}