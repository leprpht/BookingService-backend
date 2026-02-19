namespace BookingService.Profile.Dtos;

public class UserDto
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? ProfilePictureUrl { get; init; }
    public DateOnly DateOfBirth { get; set; }
    public required string Role { get; init; }
}