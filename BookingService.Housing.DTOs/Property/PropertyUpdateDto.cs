namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyUpdateDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string Description { get; set; }
    public int OwnerId { get; set; }
}