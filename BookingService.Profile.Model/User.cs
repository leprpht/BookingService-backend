using BookingService.Housing.Models;

namespace BookingService.Profile.Model;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required string Password { get; init; }
    public string? ProfilePictureUrl { get; set; }
    public required string Salt { get; init; }
    public DateOnly DateOfBirth { get; set; }
    public required string Role { get; set; } = "User";
    
    public ICollection<Stay> Stays { get; init; } = new List<Stay>();
    public ICollection<PropertyReview> Reviews { get; init; } = new List<PropertyReview>();
    public ICollection<Property> Properties { get; init; } = new List<Property>();
    public ICollection<Unit> Units { get; init; } = new List<Unit>();
}