namespace BookingService.Profile.Dtos;

public class UserNameUpdate
{
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
}