using BookingService.Search.GraphQL.Types.Unit;

namespace BookingService.Search.GraphQL.Types.Property;

public class PropertyType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public List<string> Pictures { get; set; } = new();
    public List<UnitListType> Units { get; set; } = new();
}