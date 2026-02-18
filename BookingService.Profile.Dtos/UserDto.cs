namespace BookingService.Profile.Dtos;

public class UserDto
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? PfpUrl { get; init; }
    public required string Role { get; init; }
}