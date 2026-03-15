namespace BookingService.Search.GraphQL.Types.Review;

public class PropertyReviewResponseType
{
    public Guid Id { get; set; }
    public Guid PropertyReviewId { get; set; }
    public Guid UserId { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}