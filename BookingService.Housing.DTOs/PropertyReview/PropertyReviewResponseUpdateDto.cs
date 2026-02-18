namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewResponseUpdateDto
{
    public int Id { get; set; }
    public required string Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PropertyReviewId { get; init; }
    public int PropertyId { get; init; }
}