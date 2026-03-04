namespace BookingService.Auth.Services;

public class RefreshTokenRequestDto
{
    public required string RefreshToken { get; init; }
}