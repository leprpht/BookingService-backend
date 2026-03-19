namespace BookingService.Search.GraphQL.Types.Review;

public class PropertyReviewType
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PropertyReviewResponseType> PropertyReviewResponses { get; set; } = [];
}