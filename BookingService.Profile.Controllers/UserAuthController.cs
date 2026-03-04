using BookingService.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Route("api/User/auth")]
public class UserAuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Registers a new user with the provided email and password.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await service.RegisterAsync(request.Email, request.Password, ipAddress);
        
        if (response == null)
            return BadRequest(new { message = "User already exists" });
        
        SetRefreshTokenCookie(response.RefreshToken);
        
        return Ok(new
        {
            response.AccessToken,
            response.AccessTokenExpiresAt,
            response.RefreshTokenExpiresAt
        });
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login a user",
        Description = "Logs in a user with the provided email and password.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await service.LoginAsync(request.Email, request.Password, ipAddress);
        
        if (response == null)
            return BadRequest(new { message = "Invalid email or password" });
        
        SetRefreshTokenCookie(response.RefreshToken);
        
        return Ok(new
        {
            response.AccessToken,
            response.AccessTokenExpiresAt,
            response.RefreshTokenExpiresAt
        });
    }

    [HttpPost("refresh")]
    [SwaggerOperation(
        Summary = "Refresh access token",
        Description = "Generates a new access token using a valid refresh token.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Refresh token is required" });

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await service.RefreshTokenAsync(refreshToken, ipAddress);
        
        if (response == null)
            return Unauthorized(new { message = "Invalid or expired refresh token" });
        
        SetRefreshTokenCookie(response.RefreshToken);
        
        return Ok(new
        {
            response.AccessToken,
            response.AccessTokenExpiresAt,
            response.RefreshTokenExpiresAt
        });
    }

    [HttpPost("revoke")]
    [SwaggerOperation(
        Summary = "Revoke refresh token",
        Description = "Revokes the current refresh token.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    public async Task<IActionResult> RevokeToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Refresh token is required" });

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        await service.RevokeTokenAsync(refreshToken, ipAddress);
        
        Response.Cookies.Delete("refreshToken");
        
        return Ok(new { message = "Token revoked successfully" });
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}