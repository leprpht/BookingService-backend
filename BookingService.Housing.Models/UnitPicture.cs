namespace BookingService.Housing.Models;

public class UnitPicture
{
    public int Id { get; set; }
    public string Url { get; init; } = null!;
    public bool IsCover { get; init; }

    public int UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
}