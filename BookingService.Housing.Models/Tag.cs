namespace BookingService.Housing.Models;

public sealed class Tag
{
    public Guid Id { get; set; }
    public required string Text { get; init; }

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}