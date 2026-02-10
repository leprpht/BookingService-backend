using BookingService.Auth.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IAuthService service): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var token = await service.RegisterAsync(request.Email, request.Password);
        if (token == null)
        {
            return BadRequest("User already exists");
        }
        
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await service.LoginAsync(request.Email, request.Password);
        if (token == null)
        {
            return BadRequest("Invalid email or password");
        }
        
        return Ok(new { Token = token });
    }
}