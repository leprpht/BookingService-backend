namespace BookingService.Housing.Models;

public sealed class PropertyReviewResponse
{
    public Guid Id { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; init; }

    public Guid PropertyReviewId { get; init; }
    public PropertyReview PropertyReview { get; init; } = null!;

    public Guid UserId { get; set; }

    public Guid PropertyId { get; init; }
    public Property Property { get; init; } = null!;
}