namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewResponseUpdateDto
{
    public Guid Id { get; set; }
    public required string Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid PropertyReviewId { get; init; }
    public Guid PropertyId { get; init; }
}