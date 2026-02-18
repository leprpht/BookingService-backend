using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public class StaySearchFilter
{
    public string? SearchTerm { get; set; }
    public StayStatus? Status { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
}