using BookingService.Profile.Model;

namespace BookingService.Auth.Data;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<RefreshToken?> GetByIdAsync(Guid id);
    Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId);
    Task AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
    Task RevokeAsync(Guid id, string? revokedByIp = null);
    Task RevokeAllUserTokensAsync(Guid userId, string? revokedByIp = null);
}