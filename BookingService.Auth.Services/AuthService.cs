using System.Security.Cryptography;
using BookingService.Auth.Data;
using BookingService.Profile.Model;
using BookingService.Shared.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BookingService.Auth.Services;

public class AuthService(
    IAuthRepository repository,
    IJwtTokenGenerator tokenGenerator,
    IRefreshTokenService refreshTokenService) : IAuthService
{
    public async Task<AuthResponseDto?> RegisterAsync(string email, string password, string? ipAddress = null)
    {
        if (!EmailValidator.IsValidEmail(email))
            throw new FormatException("Invalid email format.");

        var existingUser = await repository.GetByEmailAsync(email);
        if (existingUser != null)
            return null;

        var saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
        var salt = Convert.ToBase64String(saltBytes);
        var passwordHash = HashPassword(password, saltBytes);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = passwordHash,
            Salt = salt,
            Role = "User"
        };

        var userId = await repository.AddAsync(user);

        var accessToken = tokenGenerator.GenerateToken(userId, user.Email, user.Role);
        var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(userId, ipAddress);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpiresAt = DateTime.UtcNow.AddHours(1),
            RefreshTokenExpiresAt = refreshToken.ExpiresAt
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(string email, string password, string? ipAddress = null)
    {
        if (!EmailValidator.IsValidEmail(email))
            throw new FormatException("Invalid email format.");

        var existingUser = await repository.GetByEmailAsync(email);
        if (existingUser == null)
            return null;

        var saltBytes = Convert.FromBase64String(existingUser.Salt);
        var passwordHash = HashPassword(password, saltBytes);

        if (!CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(passwordHash),
                Convert.FromBase64String(existingUser.Password)))
            return null;

        var accessToken = tokenGenerator.GenerateToken(existingUser.Id, existingUser.Email, existingUser.Role);
        var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(existingUser.Id, ipAddress);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpiresAt = DateTime.UtcNow.AddHours(1),
            RefreshTokenExpiresAt = refreshToken.ExpiresAt
        };
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken, string? ipAddress = null)
    {
        var token = await refreshTokenService.ValidateRefreshTokenAsync(refreshToken);

        if (token == null)
            return null;

        var user = token.User;
        var newAccessToken = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);
        var newRefreshToken = await refreshTokenService.CreateRefreshTokenAsync(user.Id, ipAddress);

        await refreshTokenService.RevokeRefreshTokenAsync(
            refreshToken,
            ipAddress,
            newRefreshToken.Token);

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            AccessTokenExpiresAt = DateTime.UtcNow.AddHours(1),
            RefreshTokenExpiresAt = newRefreshToken.ExpiresAt
        };
    }

    public async Task RevokeTokenAsync(string refreshToken, string? ipAddress = null)
    {
        await refreshTokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress);
    }

    private static string HashPassword(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));
    }
}