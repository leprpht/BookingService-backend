namespace BookingService.Housing.Models;

public sealed class Unit
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int Capacity { get; init; }
    public decimal Price { get; init; }
    public int Size { get; init; }
    
    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
    public ICollection<UnitCustomization> Customizations { get; init; } = new List<UnitCustomization>();
    public ICollection<UnitPicture> Pictures { get; init; } = new List<UnitPicture>();
    
    public int PropertyId { get; init; }
    public Property Property { get; init; } = null!;
}