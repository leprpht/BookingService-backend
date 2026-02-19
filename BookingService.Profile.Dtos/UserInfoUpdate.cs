namespace BookingService.Profile.Dtos;

public class UserInfoUpdate
{
    public required UserNameDto Name { get; init; }
    public string? ProfilePictureUrl { get; init; }
    public DateOnly DateOfBirth { get; set; }
}