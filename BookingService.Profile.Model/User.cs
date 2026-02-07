namespace BookingService.Profile.Model;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? PfpUrl { get; init; }
}