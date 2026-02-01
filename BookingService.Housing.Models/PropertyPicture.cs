namespace BookingService.Housing.Models;

public sealed class PropertyPicture
{
    public int Id { get; init; }
    public string Url { get; init; } = null!;
    public bool IsCover { get; init; }
    
    public int PropertyId { get; init; }
    public Property Property { get; init; } = null!;
}