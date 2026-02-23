namespace BookingService.Housing.Models;

public sealed class Unit
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
    public int Size { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
    public ICollection<UnitCustomization> Customizations { get; init; } = new List<UnitCustomization>();
    public ICollection<UnitPicture> Pictures { get; init; } = new List<UnitPicture>();
    public ICollection<UnitAdditionalServices> AdditionalServices { get; init; } = new List<UnitAdditionalServices>();
    
    public Guid PropertyId { get; init; }
    public Property Property { get; init; } = null!;
    
    public Guid OwnerId { get; set; }
}