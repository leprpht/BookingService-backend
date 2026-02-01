namespace BookingService.Housing.Models;

public sealed class Property
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    
    public int OwnerId { get; init; }
    
    public ICollection<Unit> Units { get; init; } = new List<Unit>();
    public ICollection<PropertyPicture> Pictures { get; init; } = new List<PropertyPicture>();
}