using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public required string Salt { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? PfpUrl { get; init; }
    
    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
    public ICollection<PropertyReview> Reviews { get; init; } = new List<PropertyReview>();
    public ICollection<Property> Properties { get; init; } = new List<Property>();
}