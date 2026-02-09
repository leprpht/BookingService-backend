namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewUpdateDto
{
    public int Id { get; set; }
    public int Rating { get; init; }
    public string? Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }
    public int PropertyId { get; init; }
}