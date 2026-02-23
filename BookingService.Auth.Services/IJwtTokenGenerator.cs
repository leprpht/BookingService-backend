namespace BookingService.Auth.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string email, string role);
}