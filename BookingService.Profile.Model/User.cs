using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PfpUrl { get; init; } = string.Empty;
}