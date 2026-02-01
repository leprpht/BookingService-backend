using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public sealed class Guest : User
{
    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
}