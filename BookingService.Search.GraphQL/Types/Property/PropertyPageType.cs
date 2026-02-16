namespace BookingService.Search.GraphQL.Types.Property;

public class PropertyPageType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double Price { get; set; }
    public string? PictureUrl { get; set; }
    public double Rating { get; set; }
    public double RankingScore { get; set; }
    public int ReviewCount { get; set; }
    public int AvailableUnits { get; set; }
    public IEnumerable<string> Tags { get; set; } = new List<string>();
}