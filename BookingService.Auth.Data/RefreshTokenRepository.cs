using BookingService.Database;
using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Auth.Data;

public class RefreshTokenRepository(BookingServiceDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId)
    {
        return await context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(rt => rt.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task RevokeAsync(Guid id, string? revokedByIp = null)
    {
        var token = await GetByIdAsync(id);
        if (token != null && token.IsActive)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = revokedByIp;
            await UpdateAsync(token);
        }
    }

    public async Task RevokeAllUserTokensAsync(Guid userId, string? revokedByIp = null)
    {
        var activeTokens = await GetActiveTokensByUserIdAsync(userId);
        foreach (var token in activeTokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = revokedByIp;
        }

        await context.SaveChangesAsync();
    }
}