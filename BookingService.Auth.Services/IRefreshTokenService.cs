using BookingService.Profile.Model;

namespace BookingService.Auth.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    Task<RefreshToken> CreateRefreshTokenAsync(Guid userId, string? ipAddress = null);
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token, string? ipAddress = null, string? replacedByToken = null);
    Task RevokeAllUserRefreshTokensAsync(Guid userId, string? ipAddress = null);
}