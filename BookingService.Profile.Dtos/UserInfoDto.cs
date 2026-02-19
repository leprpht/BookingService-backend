namespace BookingService.Profile.Dtos;

public class UserInfoDto
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public string? ProfilePictureUrl { get; init; }
}