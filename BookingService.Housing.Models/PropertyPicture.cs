namespace BookingService.Housing.Models;

public sealed class PropertyPicture
{
    public Guid Id { get; set; }
    public string Url { get; init; } = null!;
    public bool IsCover { get; init; }
    
    public Guid PropertyId { get; set; }
    public Property Property { get; init; } = null!;
}