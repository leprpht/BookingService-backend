using BookingService.Auth.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Route("api/User/auth")]
public class UserAuthController(
    IAuthService service): ControllerBase
{
    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Registers a new user with the provided email and password.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
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
    [SwaggerOperation(
        Summary = "Login a user",
        Description = "Logs in a user with the provided email and password.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    
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