using System.Security.Cryptography;
using BookingService.Auth.Data;
using BookingService.Profile.Model;
using BookingService.Shared.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BookingService.Auth.Services;

public class AuthService(IAuthRepository repository, IJwtTokenGenerator tokenGenerator) : IAuthService
{
    public async Task<string?> RegisterAsync(string email, string password)
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

        var id = await repository.AddAsync(user);

        return tokenGenerator.GenerateToken(id, user.Email, user.Role);
    }
    
    public async Task<string?> LoginAsync(string email, string password)
    {
        if (!EmailValidator.IsValidEmail(email))
            throw new FormatException("Invalid email format.");
        
        var existingUser = await repository.GetByEmailAsync(email);
        if (existingUser == null)
            return null;

        var saltBytes = Convert.FromBase64String(existingUser.Salt);

        var passwordHash = HashPassword(password, saltBytes);
        
        return !CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(passwordHash), 
            Convert.FromBase64String(existingUser.Password))
            ? null
            : tokenGenerator.GenerateToken(existingUser.Id, existingUser.Email, existingUser.Role);
    }

    private static string HashPassword(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100_000,
            numBytesRequested: 256 / 8));
    }
}
