using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public sealed class Owner : User
{
    public ICollection<Property> Properties { get; init; } = new List<Property>();
}