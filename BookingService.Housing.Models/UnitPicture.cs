namespace BookingService.Housing.Models;

public class UnitPicture
{
    public Guid Id { get; set; }
    public string Url { get; init; } = null!;
    public bool IsCover { get; init; }

    public Guid UnitId { get; set; }
    public Unit Unit { get; init; } = null!;
}