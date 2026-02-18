namespace BookingService.Housing.Models;

public sealed class PropertyReviewResponse
{
    public int Id { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; init; }
    
    public int PropertyReviewId { get; init; }
    public PropertyReview PropertyReview { get; init; } = null!;
    
    public int UserId { get; set; }
    
    public int PropertyId { get; init; }
    public Property Property { get; init; } = null!;
}