namespace BookingService.Housing.Models;

public sealed class PropertyReview
{
    public Guid Id { get; set; }
    public int Rating { get; init; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; init; }
    
    public Guid UserId { get; set; }
    
    public Guid PropertyId { get; init; }
    public Property Property { get; init; } = null!;
    
    public ICollection<PropertyReviewResponse> PropertyReviewResponses { get; init; } = new List<PropertyReviewResponse>();
}