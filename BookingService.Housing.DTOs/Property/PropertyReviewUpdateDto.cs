namespace BookingService.Housing.DTOs.Property;

public sealed class PropertyReviewUpdateDto
{
    public int Id { get; set; }
    public int Rating { get; init; }
    public string? Comment { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? Response { get; set; }
    public int GuestId { get; init; }
    public int PropertyId { get; init; }
}