namespace BookingService.Housing.Models;

public sealed class PropertyReview
{
    public int Id { get; set; }
    public int Rating { get; init; }
    public string? Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public int GuestId { get; init; }
    
    public int PropertyId { get; init; }
    public Property Property { get; init; } = null!;
}