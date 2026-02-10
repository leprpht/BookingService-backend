namespace BookingService.Housing.Models;

public sealed class Property
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OwnerId { get; init; }
    public bool IsActive { get; set; }
    
    public double AverageRating { get; private set; }
    public int ReviewCount { get; private set; }
    public double RankingScore { get; private set; }
    
    public ICollection<Unit> Units { get; init; } = new List<Unit>();
    public ICollection<PropertyPicture> Pictures { get; init; } = new List<PropertyPicture>();
    public ICollection<PropertyReview> Reviews { get; init; } = new List<PropertyReview>();
    public ICollection<Tag> Tags { get; init; } = new List<Tag>();
    
    public void UpdateRating()
    {
        AverageRating = Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        ReviewCount = Reviews.Count;
        RankingScore = ReviewCount / (double)(ReviewCount + 3) * AverageRating + 3 / (double)(ReviewCount + 3) * 5;
    }
}