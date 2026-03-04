namespace BookingService.Auth.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(string email, string password, string? ipAddress = null);
    Task<AuthResponseDto?> LoginAsync(string email, string password, string? ipAddress = null);
    Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken, string? ipAddress = null);
    Task RevokeTokenAsync(string refreshToken, string? ipAddress = null);
}