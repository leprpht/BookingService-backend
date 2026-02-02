namespace BookingService.Profile.Dtos;

public class UserInfoDto
{
    public required int Id { get; init; }
    public required string FullName { get; init; }
    public string? PfpUrl { get; init; }
}