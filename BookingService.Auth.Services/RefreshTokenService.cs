using System.Security.Cryptography;
using BookingService.Auth.Data;
using BookingService.Profile.Model;
using Microsoft.Extensions.Configuration;

namespace BookingService.Auth.Services;

public class RefreshTokenService(
    IRefreshTokenRepository repository,
    IConfiguration configuration) : IRefreshTokenService
{
    private readonly int _refreshTokenExpiryDays = 
        configuration.GetValue<int>("Jwt:RefreshTokenExpiryDays", 7);

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(Guid userId, string? ipAddress = null)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = GenerateRefreshToken(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
            IsRevoked = false
        };

        await repository.AddAsync(refreshToken);
        return refreshToken;
    }

    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        var refreshToken = await repository.GetByTokenAsync(token);

        if (refreshToken == null || !refreshToken.IsActive)
            return null;

        return refreshToken;
    }

    public async Task RevokeRefreshTokenAsync(string token, string? ipAddress = null, string? replacedByToken = null)
    {
        var refreshToken = await repository.GetByTokenAsync(token);
        
        if (refreshToken == null || !refreshToken.IsActive)
            return;

        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = replacedByToken;

        await repository.UpdateAsync(refreshToken);
    }

    public async Task RevokeAllUserRefreshTokensAsync(Guid userId, string? ipAddress = null)
    {
        await repository.RevokeAllUserTokensAsync(userId, ipAddress);
    }
}