namespace BookingService.Profile.Dtos;

public class UserUpdateDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? PfpUrl { get; init; }
}